using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource cameraMusic;  // Arrastra aquí el AudioSource de la música de la cámara
    public AudioSource missionFailedMusic;  // Arrastra aquí el AudioSource de la música de misión fallida

    public GameObject missionFailedCanvas;  // Arrastra aquí el Canvas de misión fallida

    void Update()
    {
        // Verifica si el canvas de misión fallida está activo
        if (missionFailedCanvas.activeSelf)
        {
            // Detiene la música de la cámara y reproduce la de misión fallida
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
            // Si el canvas de misión fallida no está activo, reproduce la música de la cámara
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
