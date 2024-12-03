using UnityEngine;
using TMPro;
using System.IO;
using System.Xml.Serialization;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Solo una variable para mostrar el texto completo

    private string playerName;
    private int score;

    private void Start()
    {
        // Asegúrate de que GameData.Instance esté correctamente inicializado
        if (GameData.Instance != null)
        {
            playerName = GameData.Instance.playerName;
            LoadPlayerScore();
        }
        else
        {
            Debug.LogError("GameData instance no está inicializado.");
        }
    }

    private void LoadPlayerScore()
    {
        string profilesFolder = Path.Combine(Application.persistentDataPath, "Profiles");
        string filePath = Path.Combine(profilesFolder, playerName.Replace(" ", "_"));

        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProfileData));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                ProfileData playerData = (ProfileData)serializer.Deserialize(stream);
                score = playerData.score;
                // Concatenamos el nombre del jugador y el puntaje en un solo texto
                scoreText.text = "Jugador: " + playerName + "\nPuntaje: " + score;
            }
        }
        else
        {
            // Si no hay datos de puntaje, mostramos el nombre y el puntaje como 0
            scoreText.text = "Jugador: " + playerName + "\nPuntaje: 0";
        }
    }
}
