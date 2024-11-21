using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Image[] hearts;
    public Animator animator;
    public GameObject missionFailedCanvas;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(VisualFeedback());


        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }


        UpdateHealthUI();
        Debug.Log("Jugador recibe daño. Salud actual: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("Jugador ha muerto");

        
        animator.SetTrigger("Die");

        
        GetComponent<PlayerController>().enabled = false;

        
        StartCoroutine(ShowMissionFailedCanvas());
    }

    private IEnumerator ShowMissionFailedCanvas()
    {
        yield return new WaitForSeconds(2f); // Espera que la animación termine
        missionFailedCanvas.SetActive(true); // Mostrar el Canvas
    }

    private IEnumerator VisualFeedback()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;

        
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        
        UpdateHealthUI();
        Debug.Log("Jugador recupera salud. Salud actual: " + currentHealth);
    }

    private void UpdateHealthUI()
    {
        
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true; 
            }
            else
            {
                hearts[i].enabled = false; 
            }
        }
    }
}
