using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField playerNameInputField;
    public GameObject nameInputPanel;
    public GameObject mainMenuPanel;
    public GameObject playerListPanel;
    public GameObject highScoresPanel;
    public TMP_Text highScoresText;
    public TMP_Text playerListText;

    private List<PlayerData> players = new List<PlayerData>();

    private void Start()
    {
        LoadPlayers();
        ShowMainMenu();
        if (PlayerPrefs.GetInt("PlayerCount", 0) == 0)
        {
            // Guardar un jugador de prueba
            SavePlayer(new PlayerData { name = "TestPlayer", score = 100, highScore = 200 });
        }
        LoadPlayers();
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        nameInputPanel.SetActive(false);
        playerListPanel.SetActive(false);
        highScoresPanel.SetActive(false);
    }

    public void OpenNameInputPanel()
    {
        nameInputPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void StartGame()
    {
        string playerName = playerNameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString("CurrentPlayer", playerName);
            PlayerPrefs.SetInt("CurrentScore", 0);
            SceneManager.LoadScene("Level1");
        }
    }

    public void CancelNameInput()
    {
        playerNameInputField.text = "";
        ShowMainMenu();
    }

    public void LoadGame()
    {
        ShowPlayerList();
    }

    private void ShowPlayerList()
    {
        playerListPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        playerListText.text = "";

        foreach (var player in players)
        {
            playerListText.text += $"{player.name} - Score: {player.score}\n";
        }
    }

    public void LoadSelectedPlayer(string playerName)
    {
        PlayerData selectedPlayer = players.Find(p => p.name == playerName);
        if (selectedPlayer != null)
        {
            PlayerPrefs.SetString("CurrentPlayer", selectedPlayer.name);
            PlayerPrefs.SetInt("CurrentScore", selectedPlayer.score);
            SceneManager.LoadScene("Level1");
        }
    }

    public void HighScores()
    {
        ShowHighScores();
    }

    private void ShowHighScores()
    {
        highScoresPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        highScoresText.text = "";

        foreach (var player in players)
        {
            highScoresText.text += $"{player.name} - High Score: {player.highScore}\n";
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void LoadPlayers()
    {
        int playerCount = PlayerPrefs.GetInt("PlayerCount", 0);
        players.Clear();

        for (int i = 0; i < playerCount; i++)
        {
            string playerName = PlayerPrefs.GetString($"Player_{i}_Name", "");
            int playerScore = PlayerPrefs.GetInt($"Player_{i}_Score", 0);
            int playerHighScore = PlayerPrefs.GetInt($"Player_{i}_HighScore", 0);

            players.Add(new PlayerData
            {
                name = playerName,
                score = playerScore,
                highScore = playerHighScore
            });
        }
    }

    private void SavePlayer(PlayerData player)
    {
        int playerIndex = players.FindIndex(p => p.name == player.name);

        if (playerIndex == -1)
        {
            players.Add(player);
            PlayerPrefs.SetInt("PlayerCount", players.Count);
            playerIndex = players.Count - 1;
        }

        PlayerPrefs.SetString($"Player_{playerIndex}_Name", player.name);
        PlayerPrefs.SetInt($"Player_{playerIndex}_Score", player.score);
        PlayerPrefs.SetInt($"Player_{playerIndex}_HighScore", player.highScore);
    }

    [System.Serializable]
    private class PlayerData
    {
        public string name;
        public int score;
        public int highScore;
    }
}
