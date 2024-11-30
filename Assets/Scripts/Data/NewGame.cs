using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField profileInput;

    public void Generate()
    {
        string profileName = this.profileInput.text;
        ProfileStorage.CreateNewGame(profileName);

        if (GameData.Instance != null)
        {
            GameData.Instance.playerName = profileName;
        }
        else
        {
            Debug.LogError("GameData.Instance no está inicializado.");
            return;
        }

        SceneManager.LoadScene("NewGameCinematic");
    }
}
