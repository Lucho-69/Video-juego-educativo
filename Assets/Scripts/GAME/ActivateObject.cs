using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject objetoParaActivar; // Asigna el objeto que quieres activar en el Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivarObjeto();
        }
    }

    private void ActivarObjeto()
    {
        if (objetoParaActivar != null)
        {
            objetoParaActivar.SetActive(true);   // Activa el objeto especificado
            gameObject.SetActive(false);         // Desactiva el objeto actual que contiene este script
        }
        else
        {
            Debug.LogWarning("El objeto para activar no está asignado en el Inspector.");
        }
    }
}
