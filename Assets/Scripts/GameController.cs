using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject missionFailedCanvas;
    public KeyCode pauseKey = KeyCode.Escape;
    public TMP_Text randomFactText;
    public TMP_Text playerHealthText;

    private bool isPaused = false;
    private string[] randomFacts = {
        "Las hero�nas de la Coronilla lucharon valientemente por su libertad.",
        "Fueron lideradas por Manuela Gandarillas, una mujer ciega.",
        "La batalla de la Coronilla ocurri� el 27 de mayo de 1812.",
        "Usaron piedras, palos y utensilios de cocina como armas.",
        "El combate se llev� a cabo en la colina de la Coronilla, en Cochabamba.",
        "Enfrentaron a las tropas realistas con gran valent�a.",
        "Manuela Gandarillas se convirti� en un s�mbolo de resistencia en Bolivia.",
        "El 27 de mayo es el D�a de la Madre en Bolivia en honor a estas mujeres.",
        "Eran mujeres de todas las edades, desde adolescentes hasta ancianas.",
        "Se dice que Manuela Gandarillas grit� '�Acaso no hay donde morir mejor?' al ver al enemigo.",
        "Su sacrificio inspir� a otros movimientos independentistas en Bolivia.",
        "Se les honra con monumentos en Cochabamba como s�mbolo de hero�smo.",
        "La Coronilla se considera un s�mbolo de resistencia y valent�a en Cochabamba.",
        "Lucharon sin entrenamiento militar, pero con gran determinaci�n.",
        "Son consideradas el primer ej�rcito femenino en Am�rica Latina.",
        "La resistencia de estas mujeres fortaleci� la causa de la independencia en Bolivia.",
        "La historia de las hero�nas es un �cono cultural boliviano.",
        "Muchos poetas y escritores han inmortalizado su valent�a en obras literarias.",
        "Lucharon para proteger a sus familias y a futuras generaciones.",
        "Aunque perdieron, su coraje fue un triunfo moral para los patriotas.",
        "La colina de la Coronilla es un lugar de orgullo para los cochabambinos.",
        "Inspiraron a muchas mujeres en movimientos de empoderamiento femenino en Am�rica Latina.",
        "Enfrentaron a un ej�rcito profesional con gran dignidad.",
        "Se unieron sin esperar reconocimiento ni recompensa.",
        "La historia de las hero�nas se ense�a en las escuelas de Bolivia.",
        "Fue uno de los primeros movimientos civiles en la independencia boliviana.",
        "Son un s�mbolo de fortaleza y resiliencia para Bolivia.",
        "La batalla de la Coronilla fue parte de la guerra de independencia de Am�rica del Sur.",
        "Las hero�nas desafiaron la autoridad colonial y los estereotipos de g�nero de la �poca.",
        "A pesar de la derrota, su valent�a fue reconocida por l�deres independentistas.",
        "Se cuenta que algunas mujeres prefirieron luchar antes que rendirse o huir.",
        "La lucha en la Coronilla fue un acto espont�neo de defensa comunitaria.",
        "Se enfrentaron a soldados fuertemente armados con herramientas improvisadas.",
        "El acto de estas mujeres marc� un hito en la historia de la independencia boliviana.",
        "Su resistencia alent� a la poblaci�n a resistir la opresi�n colonial.",
        "La imagen de las hero�nas inspira a artistas en Bolivia hasta la fecha.",
        "Cochabamba es conocida como la 'Ciudad de las Hero�nas' en honor a ellas.",
        "Cada a�o, en Cochabamba, se realizan actos c�vicos en su memoria.",
        "La historia de estas mujeres muestra la importancia del rol femenino en las guerras de independencia.",
        "En su honor, varias calles, plazas y escuelas llevan sus nombres en Bolivia."
    };

    private void Start()
    {
        pauseMenuCanvas.SetActive(false);
        missionFailedCanvas.SetActive(false);
        UpdateRandomFact();
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey) && !missionFailedCanvas.activeSelf)
        {
            UpdateRandomFact();
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        
        //UpdatePlayerHealth();
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TriggerMissionFailed()
    {
        missionFailedCanvas.SetActive(true);
        Time.timeScale = 0f;
        UpdateRandomFact(); 
    }

    private void UpdateRandomFact()
    {
        int randomIndex = Random.Range(0, randomFacts.Length);
        randomFactText.text = randomFacts[randomIndex];
    }
    /*
    private void UpdatePlayerHealth()
    {
        // Aqu� debes enlazar el valor real de la vida del jugador, este es un ejemplo.
        int playerHealth = FindObjectOfType<PlayerController>().health;
        playerHealthText.text = "Health: " + playerHealth;
    }*/
}
