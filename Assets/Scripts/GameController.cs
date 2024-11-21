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
        "Las heroínas de la Coronilla lucharon valientemente por su libertad.",
        "Fueron lideradas por Manuela Gandarillas, una mujer ciega.",
        "La batalla de la Coronilla ocurrió el 27 de mayo de 1812.",
        "Usaron piedras, palos y utensilios de cocina como armas.",
        "El combate se llevó a cabo en la colina de la Coronilla, en Cochabamba.",
        "Enfrentaron a las tropas realistas con gran valentía.",
        "Manuela Gandarillas se convirtió en un símbolo de resistencia en Bolivia.",
        "El 27 de mayo es el Día de la Madre en Bolivia en honor a estas mujeres.",
        "Eran mujeres de todas las edades, desde adolescentes hasta ancianas.",
        "Se dice que Manuela Gandarillas gritó '¿Acaso no hay donde morir mejor?' al ver al enemigo.",
        "Su sacrificio inspiró a otros movimientos independentistas en Bolivia.",
        "Se les honra con monumentos en Cochabamba como símbolo de heroísmo.",
        "La Coronilla se considera un símbolo de resistencia y valentía en Cochabamba.",
        "Lucharon sin entrenamiento militar, pero con gran determinación.",
        "Son consideradas el primer ejército femenino en América Latina.",
        "La resistencia de estas mujeres fortaleció la causa de la independencia en Bolivia.",
        "La historia de las heroínas es un ícono cultural boliviano.",
        "Muchos poetas y escritores han inmortalizado su valentía en obras literarias.",
        "Lucharon para proteger a sus familias y a futuras generaciones.",
        "Aunque perdieron, su coraje fue un triunfo moral para los patriotas.",
        "La colina de la Coronilla es un lugar de orgullo para los cochabambinos.",
        "Inspiraron a muchas mujeres en movimientos de empoderamiento femenino en América Latina.",
        "Enfrentaron a un ejército profesional con gran dignidad.",
        "Se unieron sin esperar reconocimiento ni recompensa.",
        "La historia de las heroínas se enseña en las escuelas de Bolivia.",
        "Fue uno de los primeros movimientos civiles en la independencia boliviana.",
        "Son un símbolo de fortaleza y resiliencia para Bolivia.",
        "La batalla de la Coronilla fue parte de la guerra de independencia de América del Sur.",
        "Las heroínas desafiaron la autoridad colonial y los estereotipos de género de la época.",
        "A pesar de la derrota, su valentía fue reconocida por líderes independentistas.",
        "Se cuenta que algunas mujeres prefirieron luchar antes que rendirse o huir.",
        "La lucha en la Coronilla fue un acto espontáneo de defensa comunitaria.",
        "Se enfrentaron a soldados fuertemente armados con herramientas improvisadas.",
        "El acto de estas mujeres marcó un hito en la historia de la independencia boliviana.",
        "Su resistencia alentó a la población a resistir la opresión colonial.",
        "La imagen de las heroínas inspira a artistas en Bolivia hasta la fecha.",
        "Cochabamba es conocida como la 'Ciudad de las Heroínas' en honor a ellas.",
        "Cada año, en Cochabamba, se realizan actos cívicos en su memoria.",
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
        // Aquí debes enlazar el valor real de la vida del jugador, este es un ejemplo.
        int playerHealth = FindObjectOfType<PlayerController>().health;
        playerHealthText.text = "Health: " + playerHealth;
    }*/
}
