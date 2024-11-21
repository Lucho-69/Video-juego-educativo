using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerHealt : MonoBehaviour
{
    // Referencia al AudioSource del objeto actual
    public AudioSource audioSource;

    // Referencia al objeto externo, como el Player
    public GameObject player;

    // Referencia al men� que se debe mostrar cuando el Player est� desactivado
    public GameObject menu;

    // Valor de pitch cuando el Player est� desactivado
    public float pitchCuandoPlayerDesactivado = 0.5f;

    // Valor de pitch por defecto cuando el Player est� activado
    public float pitchNormal = 1.0f;

    // Velocidad de transici�n del pitch
    public float velocidadTransicion = 2.0f;

    void Update()
    {
        // Determinar el pitch objetivo basado en si el Player est� activo o no
        float pitchObjetivo = player.activeInHierarchy ? pitchNormal : pitchCuandoPlayerDesactivado;

        // Usar Mathf.Lerp para hacer una transici�n suave entre el pitch actual y el pitch objetivo
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, pitchObjetivo, Time.deltaTime * velocidadTransicion);

        // Si el player est� desactivado, mostrar el men�
        if (!player.activeInHierarchy)
        {
            MostrarMenu();
        }
    }

    // M�todo para mostrar el men�
    void MostrarMenu()
    {
        menu.SetActive(true); // Activar el objeto del men�
    }
}
