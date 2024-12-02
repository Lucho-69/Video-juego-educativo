using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource cameraMusic;  // Arrastra aqu� el AudioSource de la m�sica de la c�mara
    public AudioSource missionFailedMusic;  // Arrastra aqu� el AudioSource de la m�sica de misi�n fallida

    public GameObject missionFailedCanvas;  // Arrastra aqu� el Canvas de misi�n fallida

    void Update()
    {
        // Verifica si el canvas de misi�n fallida est� activo
        if (missionFailedCanvas.activeSelf)
        {
            // Detiene la m�sica de la c�mara y reproduce la de misi�n fallida
            if (cameraMusic.isPlaying)
            {
                cameraMusic.Stop();
            }

            if (!missionFailedMusic.isPlaying)
            {
                missionFailedMusic.Play();
            }
        }
        else
        {
            // Si el canvas de misi�n fallida no est� activo, reproduce la m�sica de la c�mara
            if (!cameraMusic.isPlaying)
            {
                cameraMusic.Play();
            }

            if (missionFailedMusic.isPlaying)
            {
                missionFailedMusic.Stop();
            }
        }
    }
}
