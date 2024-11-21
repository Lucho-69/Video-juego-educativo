using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileList : MonoBehaviour
{
    public Transform profilesHolder;
    public GameObject profileBoxUIPrefab;

    void Start()
    {
        var index = ProfileStorage.GetProfileIndex();
        foreach (var profileName in index.profileFileNames)
        {
            var go = Instantiate(this.profileBoxUIPrefab);
            var uibox = go.GetComponent<ProfileBoxUI>();

            uibox.nameLabel.text = profileName;

            uibox.loadBtn.onClick.AddListener(() =>
            {
                Debug.Log("Load");
                ProfileStorage.loadProfile(profileName);
                SceneManager.LoadScene("LoadingScene");
            });

            uibox.deleteBtn.onClick.AddListener(() =>
            {
                ProfileStorage.DeleteProfile(profileName);
                Debug.Log("Destroy");
                Destroy(go);
            });

            go.transform.SetParent(this.profilesHolder, false);
        }
    }
}
