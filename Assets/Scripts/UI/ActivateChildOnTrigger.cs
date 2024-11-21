using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChildOnTrigger : MonoBehaviour
{
    public GameObject player;            // Referencia directa al Player con el tag "Player"
    public string childObjectName;       // Nombre del objeto hijo del Player que queremos activar

    private GameObject playerChild;      // Referencia al objeto hijo que activaremos

    void Start()
    {
        // Buscar el objeto hijo dentro del Player por su nombre
        if (player != null)
        {
            playerChild = player.transform.Find(childObjectName)?.gameObject;

            // Verificamos que el objeto hijo se encontró
            if (playerChild == null)
            {
                Debug.LogWarning("No se encontró el objeto hijo con el nombre especificado en el Player.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto que entra tiene el tag "Player"
        if (collision.CompareTag("Player") && playerChild != null)
        {
            // Activar el objeto hijo del Player
            playerChild.SetActive(true);
        }
    }
}
