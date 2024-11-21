using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damageAmount = 1;  // Daño por contacto con la espada
    private int accumulatedDamage = 0;  // Daño acumulado por la espada

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Acumulamos el daño cada vez que la espada entra en contacto
                accumulatedDamage += damageAmount;

                // Verificamos si el daño acumulado alcanza 2
                if (accumulatedDamage >= 2)
                {
                    // Restamos una vida al jugador
                    playerHealth.TakeDamage(1);

                    // Reiniciamos el contador de daño acumulado
                    accumulatedDamage = 0;
                }
            }
        }
    }
}
