using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prueba : MonoBehaviour
{
    public GameObject dialogPanel; // Panel de di�logo
    public TextMeshProUGUI messageText; // Texto del mensaje
    public TextMeshProUGUI interactSymbol; // S�mbolo de interacci�n
    public string[] messages; // Lista de mensajes para los di�logos
    public string sceneToLoad; // Escena a cargar (opcional)
    public KeyCode interactKey = KeyCode.E; // Tecla de interacci�n
    public float typingSpeed = 0.05f; // Velocidad de escritura

    private bool isPlayerNearby = false; // �El jugador est� cerca?
    private bool isDisplayingMessage = false; // �Se est� mostrando un mensaje?
    private int currentMessageIndex = 0; // �ndice del mensaje actual

    private void Start()
    {
        dialogPanel.SetActive(false); // Ocultar el panel al inicio
        interactSymbol.gameObject.SetActive(false); // Ocultar s�mbolo de interacci�n
    }

    private void Update()
    {
        if (isPlayerNearby)
        {
            interactSymbol.gameObject.SetActive(true); // Mostrar s�mbolo de interacci�n

            if (Input.GetKeyDown(interactKey))
            {
                if (!isDisplayingMessage)
                {
                    StartDialogue(); // Iniciar di�logo
                }
                else
                {
                    SkipTypingOrContinue(); // Saltar texto o avanzar al siguiente
                }
            }
        }
        else
        {
            interactSymbol.gameObject.SetActive(false); // Ocultar s�mbolo si el jugador no est� cerca
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
            dialogPanel.SetActive(false); // Ocultar el panel de di�logo
            interactSymbol.gameObject.SetActive(false); // Ocultar el s�mbolo de interacci�n
            StopAllCoroutines(); // Detener cualquier texto en escritura
            messageText.text = ""; // Limpiar el texto
            isDisplayingMessage = false; // Resetear estado del di�logo
            currentMessageIndex = 0; // Reiniciar �ndice del mensaje
        }
    }

    private void StartDialogue()
    {
        isDisplayingMessage = true; // Indicar que se est� mostrando un mensaje
        dialogPanel.SetActive(true); // Mostrar el panel de di�logo
        currentMessageIndex = 0; // Reiniciar el �ndice de mensajes
        StartCoroutine(TypeMessage(messages[currentMessageIndex])); // Iniciar escritura del primer mensaje
    }

    private IEnumerator TypeMessage(string message)
    {
        messageText.text = ""; // Limpiar el texto antes de empezar

        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter; // Escribir letra por letra
            yield return new WaitForSeconds(typingSpeed); // Esperar seg�n la velocidad configurada
        }
    }

    private void SkipTypingOrContinue()
    {
        StopAllCoroutines(); // Detener la escritura en curso

        if (messageText.text != messages[currentMessageIndex])
        {
            // Mostrar todo el mensaje actual si el jugador interrumpe la escritura
            messageText.text = messages[currentMessageIndex];
        }
        else
        {
            // Avanzar al siguiente mensaje
            currentMessageIndex++;

            if (currentMessageIndex < messages.Length)
            {
                StartCoroutine(TypeMessage(messages[currentMessageIndex]));
            }
            else
            {
                EndDialogue(); // Finalizar di�logo si no hay m�s mensajes
            }
        }
    }

    private void EndDialogue()
    {
        isDisplayingMessage = false; // Indicar que termin� el di�logo
        dialogPanel.SetActive(false); // Ocultar el panel de di�logo

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Cargar escena si est� configurada
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
