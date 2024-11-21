using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SonidosPausa : MonoBehaviour
{
    public static SonidosPausa instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para ajustar el volumen global
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    // Método para silenciar el audio
    public void MuteAudio()
    {
        AudioListener.volume = 0f;
    }

    // Método para reactivar el audio
    public void UnmuteAudio()
    {
        AudioListener.volume = 1f;
    }
}

