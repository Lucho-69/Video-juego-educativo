using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;
    private List<string> storyParts;
    private int storyIndex = 0;

    private void Start()
    {
        // Obtener el nombre del jugador desde PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "Jugador");

        // Configurar las partes de la historia
        storyParts = new List<string>
        {
            $"Bienvenido, {playerName}. Te encuentras en un momento crucial de nuestra historia. En estas tierras de coraje y resistencia, las Hero�nas de la Coronilla defendieron con valent�a lo que m�s amaban. Eran madres, hijas y amigas, unidas por un prop�sito inquebrantable.",
            "Corr�a el a�o 1812, y las fuerzas enemigas avanzaban implacables hacia Cochabamba. La amenaza era clara y aterradora. Sin embargo, estas mujeres, lideradas por la visionaria Manuela Gandarillas, decidieron enfrentar lo imposible. A pesar de su ceguera, Manuela pod�a ver claramente la necesidad de defender a su gente.",
            "Estas hero�nas no pose�an armas ni armaduras. Su arsenal estaba hecho de piedras, palos y la convicci�n de que cada una de sus acciones servir�a para un futuro libre. Contra soldados entrenados y bien equipados, las Hero�nas de la Coronilla decidieron luchar con toda su determinaci�n.",
            "Su sacrificio fue enorme, y su valent�a inspiradora. Hoy, t� tambi�n tienes una misi�n importante. No solo revives su historia, sino que debes honrar su memoria en cada paso que das en este camino.",
            $"Ahora, {playerName}, prep�rate para enfrentar los desaf�os que estos tiempos te presentar�n. Con cada nivel, descubrir�s m�s sobre sus luchas y sus sacrificios. Avanza y demuestra que su esp�ritu vive en ti."
        };

        // Mostrar la primera parte de la historia
        ShowStoryPart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            storyIndex++;
            if (storyIndex < storyParts.Count)
            {
                ShowStoryPart();
            }
            else
            {
                EndIntroduction();
            }
        }
    }

    private void ShowStoryPart()
    {
        welcomeText.text = storyParts[storyIndex];
    }

    private void EndIntroduction()
    {
        // Cambiar a la escena del nivel 1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}
