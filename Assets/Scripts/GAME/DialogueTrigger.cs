using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI interactSymbol;
    public string[] messages;
    public KeyCode interactKey = KeyCode.E;
    public MissionTrigger missionTrigger;
    public PlayerController playerController; // Asegúrate de asignar el script del jugador.
    public float typingSpeed = 0.05f;

    private bool isPlayerNearby = false;
    private bool isDialogueActive = false;
    private int currentMessageIndex = 0;

    private void Start()
    {
        dialogPanel.SetActive(false);
        interactSymbol.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNearby)
        {
            interactSymbol.gameObject.SetActive(!isDialogueActive);

            if (Input.GetKeyDown(interactKey) && !isDialogueActive)
            {
                StartDialogue();
            }
            else if (Input.GetKeyDown(interactKey) && isDialogueActive)
            {
                ContinueDialogue();
            }
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
            interactSymbol.gameObject.SetActive(false); // Oculta el símbolo de interacción.
            EndDialogue();
        }
    }

    private void StartDialogue()
    {
        if (playerController != null)
        {
            playerController.enabled = false; // Bloquea el movimiento del jugador.
        }

        isDialogueActive = true;
        dialogPanel.SetActive(true);
        currentMessageIndex = 0;
        StartCoroutine(TypeMessage(messages[currentMessageIndex]));
    }

    private void ContinueDialogue()
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
                missionTrigger.StartMission(); // Inicia la misión solo después de completar el diálogo.
            }
        }
    }

    private IEnumerator TypeMessage(string message)
    {
        messageText.text = "";
        foreach (char letter in message)
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        if (playerController != null)
        {
            playerController.enabled = true; // Reactiva el movimiento del jugador.
        }

        isDialogueActive = false;
        dialogPanel.SetActive(false);
        interactSymbol.gameObject.SetActive(false); // Limpieza adicional.
    }

}
