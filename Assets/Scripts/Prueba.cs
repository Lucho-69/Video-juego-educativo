using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prueba : MonoBehaviour
{
    public GameObject dialogPanel; // Panel de diálogo
    public TextMeshProUGUI messageText; // Texto del mensaje
    public TextMeshProUGUI interactSymbol; // Símbolo de interacción
    public string[] messages; // Lista de mensajes para los diálogos
    public string sceneToLoad; // Escena a cargar (opcional)
    public KeyCode interactKey = KeyCode.E; // Tecla de interacción
    public float typingSpeed = 0.05f; // Velocidad de escritura

    private bool isPlayerNearby = false; // ¿El jugador está cerca?
    private bool isDisplayingMessage = false; // ¿Se está mostrando un mensaje?
    private int currentMessageIndex = 0; // Índice del mensaje actual

    private void Start()
    {
        dialogPanel.SetActive(false); // Ocultar el panel al inicio
        interactSymbol.gameObject.SetActive(false); // Ocultar símbolo de interacción
    }

    private void Update()
    {
        if (isPlayerNearby)
        {
            interactSymbol.gameObject.SetActive(true); // Mostrar símbolo de interacción

            if (Input.GetKeyDown(interactKey))
            {
                if (!isDisplayingMessage)
                {
                    StartDialogue(); // Iniciar diálogo
                }
                else
                {
                    SkipTypingOrContinue(); // Saltar texto o avanzar al siguiente
                }
            }
        }
        else
        {
            interactSymbol.gameObject.SetActive(false); // Ocultar símbolo si el jugador no está cerca
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
            dialogPanel.SetActive(false); // Ocultar el panel de diálogo
            interactSymbol.gameObject.SetActive(false); // Ocultar el símbolo de interacción
            StopAllCoroutines(); // Detener cualquier texto en escritura
            messageText.text = ""; // Limpiar el texto
            isDisplayingMessage = false; // Resetear estado del diálogo
            currentMessageIndex = 0; // Reiniciar índice del mensaje
        }
    }

    private void StartDialogue()
    {
        isDisplayingMessage = true; // Indicar que se está mostrando un mensaje
        dialogPanel.SetActive(true); // Mostrar el panel de diálogo
        currentMessageIndex = 0; // Reiniciar el índice de mensajes
        StartCoroutine(TypeMessage(messages[currentMessageIndex])); // Iniciar escritura del primer mensaje
    }

    private IEnumerator TypeMessage(string message)
    {
        messageText.text = ""; // Limpiar el texto antes de empezar

        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter; // Escribir letra por letra
            yield return new WaitForSeconds(typingSpeed); // Esperar según la velocidad configurada
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
                EndDialogue(); // Finalizar diálogo si no hay más mensajes
            }
        }
    }

    private void EndDialogue()
    {
        isDisplayingMessage = false; // Indicar que terminó el diálogo
        dialogPanel.SetActive(false); // Ocultar el panel de diálogo

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Cargar escena si está configurada
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
