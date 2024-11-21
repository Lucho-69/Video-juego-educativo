using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSistem : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    void Start()
    {
        // Obtener el componente ParticleSystem en el objeto
        particleSystem = GetComponent<ParticleSystem>();

        // Asegurar que el sistema de partículas use unscaledTime
        var main = particleSystem.main;
        main.useUnscaledTime = true;
    }
}
