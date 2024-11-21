using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prueba : MonoBehaviour
{
    public Canvas mensajeCanvas;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI interactSymbol; 
    public string sceneToLoad;
    public string directMessage = "";
    public KeyCode interactKey = KeyCode.E; 
    public float typingSpeed = 0.05f;

    private bool isPlayerNearby = false;
    private bool isDisplayingMessage = false;

    private void Start()
    {
       
        mensajeCanvas.gameObject.SetActive(false);
        interactSymbol.gameObject.SetActive(false);
    }

    private void Update()
    {
        
        if (isPlayerNearby)
        {
            interactSymbol.gameObject.SetActive(true);

            
            if (Input.GetKeyDown(interactKey) && !isDisplayingMessage)
            {
                StartCoroutine(DisplayMessage());
            }
        }
        else
        {
            interactSymbol.gameObject.SetActive(false);
        }
    }

    private IEnumerator DisplayMessage()
    {
        isDisplayingMessage = true;
        messageText.text = ""; 
        mensajeCanvas.gameObject.SetActive(true);

        
        foreach (char letter in directMessage.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        
        yield return new WaitForSeconds(1f);

        ChangeScene();
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            mensajeCanvas.gameObject.SetActive(true); 
            messageText.text = "";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            mensajeCanvas.gameObject.SetActive(false);
            interactSymbol.gameObject.SetActive(false);
            isDisplayingMessage = false;
            StopAllCoroutines();
            messageText.text = "";
        }
    }
}
