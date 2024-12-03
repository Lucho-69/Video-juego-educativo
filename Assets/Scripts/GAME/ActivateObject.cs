using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public GameObject objetoParaActivar;

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
            objetoParaActivar.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("El objeto para activar no está asignado en el Inspector.");
        }
    }
}
