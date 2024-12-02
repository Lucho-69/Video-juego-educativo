using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prueba : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI interactSymbol;
    public string[] messages;
    public string sceneToLoad;
    public KeyCode interactKey = KeyCode.E;
    public float typingSpeed = 0.05f;

    private bool isPlayerNearby = false;
    private bool isDisplayingMessage = false;
    private int currentMessageIndex = 0;
    private PlayerController playerController;
    private MissionManager missionManager;

    private void Start()
    {
        dialogPanel.SetActive(false);
        interactSymbol.gameObject.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();  // Obtener la referencia al PlayerController
        missionManager = FindObjectOfType<MissionManager>();  // Obtener la referencia al MissionManager
    }

    private void Update()
    {
        if (isPlayerNearby)
        {
            interactSymbol.gameObject.SetActive(true);

            if (Input.GetKeyDown(interactKey))
            {
                if (!isDisplayingMessage)
                {
                    StartDialogue();
                }
                else
                {
                    SkipTypingOrContinue();
                }
            }
        }
        else
        {
            interactSymbol.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            dialogPanel.SetActive(false);
            interactSymbol.gameObject.SetActive(false);
            StopAllCoroutines();
            messageText.text = "";
            isDisplayingMessage = false;
            currentMessageIndex = 0;
        }
    }

    private void StartDialogue()
    {
        // Bloquear el movimiento del jugador al iniciar el diálogo
        if (playerController != null)
        {
            playerController.enabled = false;  // Desactivar el script del jugador para bloquear el movimiento
        }

        isDisplayingMessage = true;
        dialogPanel.SetActive(true);
        currentMessageIndex = 0;
        StartCoroutine(TypeMessage(messages[currentMessageIndex]));

        // Detener el temporizador de la misión cuando se inicie el diálogo
        if (missionManager != null)
        {
            missionManager.StopMissionTimer();  // Detener el temporizador de la misión
        }

        // Marcar la misión como completada cuando inicie el diálogo
        if (missionManager != null)
        {
            missionManager.CompleteMission();  // Marcar la misión como completada
        }
    }

    private IEnumerator TypeMessage(string message)
    {
        messageText.text = "";

        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void SkipTypingOrContinue()
    {
        StopAllCoroutines();

        if (messageText.text != messages[currentMessageIndex])
        {
            messageText.text = messages[currentMessageIndex];
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
            }
        }
    }

    private void EndDialogue()
    {
        // Reactivar el movimiento del jugador después de terminar el diálogo
        if (playerController != null)
        {
            playerController.enabled = true;  // Reactivar el script del jugador para permitir el movimiento
        }

        isDisplayingMessage = false;
        dialogPanel.SetActive(false);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
