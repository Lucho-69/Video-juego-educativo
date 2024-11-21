using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Interaction : MonoBehaviour
{
    public string message = "";
    public float typingSpeed = 0.05f;
    public KeyCode interactKey = KeyCode.E;
    public float detectionRange = 3f;
    public float patrolSpeed = 2f;
    public float patrolRange = 5f;
    public string missionName = "";

    private TextMeshProUGUI messageText;
    public TextMeshProUGUI interactSymbol;
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

    private void Start()
    {
        messageText = GameObject.Find("MessageText").GetComponent<TextMeshProUGUI>();
        interactSymbol = GameObject.Find("InteractSymbol").GetComponent<TextMeshProUGUI>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_rigidbody == null)
        {
            Debug.LogWarning("Rigidbody2D no encontrado en el NPC. El movimiento de patrullaje no funcionará sin un Rigidbody2D.");
        }

        initialPosition = transform.position;
        messageText.text = "";
        interactSymbol.gameObject.SetActive(false);

        InvokeRepeating(nameof(DetectPlayer), 0, 0.5f);
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
            StopMovement(); // Detener movimiento del NPC
        }

        if (isPlayerNearby && Input.GetKeyDown(interactKey) && !isDisplayingMessage)
        {
            StartCoroutine(DisplayMessage());
        }
    }

    private void Patrol()
    {
        if (_rigidbody == null) return;

        float distanceFromStart = Vector2.Distance(initialPosition, transform.position);

        // Evitar que el NPC cambie de dirección repetidamente
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
            messageText.text = "";
            isDisplayingMessage = false;
            StopAllCoroutines();

            if (playerController != null)
                playerController.enabled = true;

            _isPatrolling = true;
            _animator.SetBool("Run", true);
        }
    }

    private IEnumerator DisplayMessage()
    {
        isDisplayingMessage = true;
        messageText.text = "";

        if (playerController != null)
            playerController.enabled = false;

        _animator.SetBool("Run", false);

        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            if (Input.GetKeyDown(interactKey))
            {
                messageText.text = message;
            }
        }

        if (playerController != null)
            playerController.enabled = true;

        isDisplayingMessage = false;
        if (missionName == "si")
        {
            FindObjectOfType<MissionManager>().StartMission();
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;

        if ((_facingRight && direction.x < 0) || (!_facingRight && direction.x > 0))
        {
            Flip(); // Cambiar la dirección del NPC solo si es necesario
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;  // Cambiar el estado de la dirección
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
