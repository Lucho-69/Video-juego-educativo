using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El personaje a seguir
    public Vector3 offset; // Mantener la distancia inicial entre la cámara y el personaje

    private Vector3 initialPosition;

    void Start()
    {
        // Guardamos la posición inicial de la cámara
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Mantener la posición inicial más el desplazamiento del personaje
            transform.position = initialPosition + (target.position + offset);
        }
    }
}
