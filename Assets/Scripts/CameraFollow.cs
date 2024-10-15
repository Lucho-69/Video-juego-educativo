using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El personaje a seguir
    public Vector3 offset; // Mantener la distancia inicial entre la c�mara y el personaje

    private Vector3 initialPosition;

    void Start()
    {
        // Guardamos la posici�n inicial de la c�mara
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Mantener la posici�n inicial m�s el desplazamiento del personaje
            transform.position = initialPosition + (target.position + offset);
        }
    }
}
