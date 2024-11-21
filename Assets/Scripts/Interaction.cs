using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Interaction : MonoBehaviour
{
    public string[] messages; // Lista de mensajes para los diálogos
    public float typingSpeed = 0.05f;
    public KeyCode interactKey = KeyCode.E;
    public float detectionRange = 3f;
    public float patrolSpeed = 2f;
    public float patrolRange = 5f;

    [SerializeField] private GameObject dialogPanel; // Panel de diálogo
    [SerializeField] private TMP_Text dialogText;    // Texto del panel de diálogo

    private TextMeshProUGUI interactSymbol;
    private bool isPlayerNearby = false;
    private bool isDisplayingMessage = false;
    private Transform playerTransform;
    private PlayerController playerController;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Vector3 initialPosition;
    private bool _facingRight = true;
    private bool _isPatrolling = true;
    private bool _isTurningAround = false;
    private int currentMessageIndex = 0; // Índice del mensaje actual

    public bool startsMission = false;
    public string missionId; // Identificador opcional para la misión (por si hay más de una)
    private MissionManager missionManager; // Referencia al MissionManager

    private void Start()
    {
        interactSymbol = GameObject.Find("InteractSymbol").GetComponent<TextMeshProUGUI>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        initialPosition = transform.position;
        interactSymbol.gameObject.SetActive(false);
        dialogPanel.SetActive(false);

        InvokeRepeating(nameof(DetectPlayer), 0, 0.5f);

        missionManager = FindObjectOfType<MissionManager>();
    }

    private void Update()
    {
        if (_isPatrolling && !isPlayerNearby && !isDisplayingMessage)
        {
            Patrol();
        }
        else if (isPlayerNearby)
        {
            FacePlayer();
            StopMovement();
        }

        if (isPlayerNearby && Input.GetKeyDown(interactKey) && !isDisplayingMessage)
        {
            StartDialogue();
        }
        else if (isDisplayingMessage && Input.GetKeyDown(interactKey))
        {
            SkipTypingOrContinue();
        }
    }

    private void Patrol()
    {
        if (_rigidbody == null) return;

        float distanceFromStart = Vector2.Distance(initialPosition, transform.position);

        if (distanceFromStart >= patrolRange && !_isTurningAround)
        {
            Flip();
            _isTurningAround = true;
        }

        if (distanceFromStart < patrolRange)
        {
            _isTurningAround = false;
        }

        float patrolDirection = _facingRight ? 1 : -1;
        _rigidbody.velocity = new Vector2(patrolDirection * patrolSpeed, _rigidbody.velocity.y);

        _animator.SetBool("Run", true);
    }

    private void StopMovement()
    {
        _rigidbody.velocity = Vector2.zero;
        _animator.SetBool("Run", false);
    }

    private void DetectPlayer()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= detectionRange)
        {
            isPlayerNearby = true;
            interactSymbol.gameObject.SetActive(true);
            _isPatrolling = false;
        }
        else
        {
            isPlayerNearby = false;
            interactSymbol.gameObject.SetActive(false);
            if (!isDisplayingMessage) _isPatrolling = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
            playerController = collision.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactSymbol.gameObject.SetActive(false);
            dialogPanel.SetActive(false);
            isDisplayingMessage = false;
            StopAllCoroutines();

            if (playerController != null)
                playerController.enabled = true;

            _isPatrolling = true;
            _animator.SetBool("Run", true);
        }
    }

    private void StartDialogue()
    {
        isDisplayingMessage = true;
        dialogPanel.SetActive(true);
        currentMessageIndex = 0;

        if (playerController != null)
            playerController.enabled = false; // Desactivar controles del jugador

        StartCoroutine(TypeMessage(messages[currentMessageIndex]));
    }

    private IEnumerator TypeMessage(string message)
    {
        dialogText.text = "";

        foreach (char letter in message.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    private void SkipTypingOrContinue()
    {
        StopAllCoroutines();

        if (dialogText.text != messages[currentMessageIndex])
        {
            dialogText.text = messages[currentMessageIndex];
        }
        else
        {
            currentMessageIndex++;

            if (currentMessageIndex < messages.Length)
            {
                StartCoroutine(TypeMessage(messages[currentMessageIndex]));
            }
            else
            {
                EndDialogue();

                // Nuevo: Iniciar la misión si este NPC está marcado
                if (startsMission && missionManager != null)
                {
                    missionManager.StartMission();
                }
            }
        }
    }

    private void EndDialogue()
    {
        isDisplayingMessage = false;
        dialogPanel.SetActive(false);

        if (playerController != null)
            playerController.enabled = true; // Reactivar controles del jugador
    }

    private void FacePlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;

        if ((_facingRight && direction.x < 0) || (!_facingRight && direction.x > 0))
        {
            Flip();
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(initialPosition, patrolRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
