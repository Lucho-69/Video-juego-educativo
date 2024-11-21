using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingPanel; // Panel de carga
    public Slider slider; // Deslizador de progreso
    public float loadDuration = 5f; // Duración del llenado del deslizador (5 segundos por defecto)

    // Ahora puedes especificar el nombre de la escena desde el Inspector o código
    public string sceneToLoad;

    void Start()
    {
        // Verificar si se ha especificado un nombre de escena, de lo contrario mostrar advertencia
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            StartCoroutine(LoadAsynchronously(sceneToLoad));
        }
        else
        {
            Debug.LogWarning("No se ha especificado ninguna escena para cargar.");
        }
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        // Iniciar la carga de la escena en segundo plano
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false; // Evitar que la escena se active automáticamente

        // Activar el panel de carga
        loadingPanel.SetActive(true);

        // Llenar el deslizador en 5 segundos
        float elapsedTime = 0f;

        while (elapsedTime < loadDuration)
        {
            // Incrementar el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Actualizar el valor del deslizador (de 0 a 1 en 5 segundos)
            slider.value = Mathf.Clamp01(elapsedTime / loadDuration);

            yield return null;
        }

        // Activar la escena una vez que el deslizador se haya llenado completamente
        operation.allowSceneActivation = true;
    }

}
