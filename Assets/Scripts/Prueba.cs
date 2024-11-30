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

    private void Start()
    {
        dialogPanel.SetActive(false); 
        interactSymbol.gameObject.SetActive(false); 
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
        isDisplayingMessage = true;
        dialogPanel.SetActive(true);
        currentMessageIndex = 0;
        StartCoroutine(TypeMessage(messages[currentMessageIndex]));
    }

    private IEnumerator TypeMessage(string message)
    {
        messageText.text = ""; // Limpiar el texto antes de empezar

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
        isDisplayingMessage = false;
        dialogPanel.SetActive(false);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
