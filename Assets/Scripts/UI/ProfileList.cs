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
                Debug.Log($"Cargando perfil: {profileName}");
                ProfileStorage.LoadProfile(profileName);

                // Aquí asignamos el nombre del perfil seleccionado a GameData
                if (GameData.Instance != null)
                {
                    GameData.Instance.playerName = profileName;
                    Debug.Log($"Nombre del perfil cargado en GameData: {GameData.Instance.playerName}");
                }
                else
                {
                    Debug.LogError("GameData no está inicializado.");
                }
                if (ProfileStorage.s_currentProfile != null)
                {
                    int currentLevel = ProfileStorage.s_currentProfile.currentLevel;
                    string levelSceneName = "Level" + currentLevel;

                    if (Application.CanStreamedLevelBeLoaded(levelSceneName))
                    {
                        Debug.Log($"Cargando escena: {levelSceneName}");
                        SceneManager.LoadScene(levelSceneName);
                    }
                    else
                    {
                        Debug.LogError($"La escena {levelSceneName} no existe.");
                    }
                }
                else
                {
                    Debug.LogError("El perfil no pudo ser cargado.");
                }
            });

            uibox.deleteBtn.onClick.AddListener(() =>
            {
                ProfileStorage.DeleteProfile(profileName);
                Debug.Log($"Perfil eliminado: {profileName}");
                Destroy(go);
            });

            go.transform.SetParent(this.profilesHolder, false);
        }
    }
}
