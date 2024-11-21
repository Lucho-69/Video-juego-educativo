using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int damageAmount = 1;
    private int accumulatedDamage = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {

                accumulatedDamage += damageAmount;


                if (accumulatedDamage >= 2)
                {

                    playerHealth.TakeDamage(1);


                    accumulatedDamage = 0;
                }
            }
        }
    }
}
