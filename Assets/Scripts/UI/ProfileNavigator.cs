using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileNavigator : MonoBehaviour
{
    // Start is called before the first frame update
   public void GoToNewGame ()
    {
        SceneManager.LoadScene("P_NewGame");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("P_Menu");
    }
}
