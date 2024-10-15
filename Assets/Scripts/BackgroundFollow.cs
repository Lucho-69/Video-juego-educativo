using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform target; // El personaje a seguir
    public Vector3 offset; // Mantener la distancia inicial entre la c�mara y el personaje

    private Vector3 initialPosition; // La posici�n inicial de la c�mara

    void Start()
    {
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = initialPosition + (target.position + offset);
        }
    }
}
