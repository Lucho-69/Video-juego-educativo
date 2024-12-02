using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource cameraMusic;
    public AudioSource missionFailedMusic;

    public GameObject missionFailedCanvas;

    void Update()
    {

        if (missionFailedCanvas.activeSelf)
        {
            
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
