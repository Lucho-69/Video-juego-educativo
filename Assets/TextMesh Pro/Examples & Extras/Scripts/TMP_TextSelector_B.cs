using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class SignInteraction : MonoBehaviour
{
    public Canvas canvas; // Canvas que contiene MessageText e InteractSymbol
    public string message = "Este es el mensaje que se mostrará lentamente.";
    public float typingSpeed = 0.05f;
    public KeyCode interactKey = KeyCode.E;

    private TextMeshProUGUI messageText;
    private TextMeshProUGUI interactSymbol;
    private bool isPlayerNearby = false;
    private bool isDisplayingMessage = false;

    private void Start()
    {
        // Buscar los componentes de texto en el Canvas y desactivar Canvas al inicio
        messageText = canvas.transform.Find("MessageText").GetComponent<TextMeshProUGUI>();
        interactSymbol = canvas.transform.Find("InteractSymbol").GetComponent<TextMeshProUGUI>();
        canvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Si el jugador está cerca y presiona la tecla de interacción, mostrar el mensaje
        if (isPlayerNearby && Input.GetKeyDown(interactKey) && !isDisplayingMessage)
        {
            StartCoroutine(DisplayMessage());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            canvas.gameObject.SetActive(true); // Activar el Canvas cuando el jugador esté cerca
            interactSymbol.gameObject.SetActive(true); // Mostrar el símbolo de interacción
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            canvas.gameObject.SetActive(false); // Desactivar el Canvas cuando el jugador se aleje
            messageText.text = ""; // Limpiar el mensaje
            isDisplayingMessage = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator DisplayMessage()
    {
        isDisplayingMessage = true;
        interactSymbol.gameObject.SetActive(false); // Ocultar el símbolo de interacción
        messageText.text = ""; // Limpiar texto antes de mostrar mensaje

        // Mostrar el mensaje letra por letra
        foreach (char letter in message.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isDisplayingMessage = false;
    }
}
