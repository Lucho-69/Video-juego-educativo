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

    // Referencia al menú que se debe mostrar cuando el Player esté desactivado
    public GameObject menu;

    // Valor de pitch cuando el Player está desactivado
    public float pitchCuandoPlayerDesactivado = 0.5f;

    // Valor de pitch por defecto cuando el Player está activado
    public float pitchNormal = 1.0f;

    // Velocidad de transición del pitch
    public float velocidadTransicion = 2.0f;

    void Update()
    {
        // Determinar el pitch objetivo basado en si el Player está activo o no
        float pitchObjetivo = player.activeInHierarchy ? pitchNormal : pitchCuandoPlayerDesactivado;

        // Usar Mathf.Lerp para hacer una transición suave entre el pitch actual y el pitch objetivo
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, pitchObjetivo, Time.deltaTime * velocidadTransicion);

        // Si el player está desactivado, mostrar el menú
        if (!player.activeInHierarchy)
        {
            MostrarMenu();
        }
    }

    // Método para mostrar el menú
    void MostrarMenu()
    {
        menu.SetActive(true); // Activar el objeto del menú
    }
}
