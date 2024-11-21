using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public Canvas mensajeCanvas; // Canvas que contiene el mensaje y el s�mbolo de interacci�n
    public string message = "Este es el mensaje que se mostrar� lentamente."; // Mensaje a mostrar
    public float typingSpeed = 0.05f; // Velocidad de escritura del mensaje
    public KeyCode interactKey = KeyCode.E; // Tecla para interactuar
    public string nextSceneName = "Principal"; // Nombre de la siguiente escena

    private TextMeshProUGUI messageText; // Componente de texto del mensaje
    public TextMeshProUGUI interactSymbol; // Componente de texto del s�mbolo de interacci�n
    private bool isPlayerNearby = false; // Indica si el jugador est� cerca
    private bool isDisplayingMessage = false; // Indica si se est� mostrando el mensaje

    private void Start()
    {
        if (mensajeCanvas != null)
        {
            // Encontrar los elementos dentro del Canvas
            messageText = mensajeCanvas.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();
            interactSymbol = mensajeCanvas.transform.Find("InteractSymbol").GetComponent<TextMeshProUGUI>();

            // Asegurarse de que el Canvas est� desactivado al inicio
            mensajeCanvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("No se encontr� el Canvas 'mensajeCanvas'. Asigna un Canvas en el Inspector.");
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactKey) && !isDisplayingMessage)
        {
            StartCoroutine(DisplayMessage());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && mensajeCanvas != null)
        {
            isPlayerNearby = true;
            mensajeCanvas.gameObject.SetActive(true); // Activar el Canvas cuando el jugador est� cerca
            interactSymbol.gameObject.SetActive(true); // Mostrar el s�mbolo de interacci�n
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && mensajeCanvas != null)
        {
            isPlayerNearby = false;
            mensajeCanvas.gameObject.SetActive(false); // Desactivar el Canvas cuando el jugador se aleje
            messageText.text = ""; // Limpiar el mensaje
            interactSymbol.gameObject.SetActive(false); // Ocultar el s�mbolo de interacci�n
            isDisplayingMessage = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator DisplayMessage()
    {
        isDisplayingMessage = true;
        interactSymbol.gameObject.SetActive(false); // Ocultar el s�mbolo de interacci�n
        messageText.text = ""; // Limpiar texto antes de mostrar mensaje

        // Mostrar el mensaje letra por letra
        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Esperar un segundo para dar tiempo a leer el mensaje
        yield return new WaitForSeconds(1f);

        // Cambiar a la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
