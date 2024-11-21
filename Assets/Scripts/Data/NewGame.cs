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
        SceneManager.LoadScene("NewGameCinematic");
    }
}
