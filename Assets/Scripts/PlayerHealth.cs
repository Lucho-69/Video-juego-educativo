using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Salud m�xima
    private int currentHealth; // Salud actual

    public Image[] hearts; // Array de im�genes de corazones
    public Animator animator; // Referencia al Animator
    public GameObject missionFailedCanvas; // Referencia al Canvas de "Mission Failed"
    private SpriteRenderer _renderer; // Para efectos visuales

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
        // Visual Feedback
        StartCoroutine(VisualFeedback());

        // Verificar si el jugador muri�
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        // Actualizar UI
        UpdateHealthUI();
        Debug.Log("Jugador recibe da�o. Salud actual: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("Jugador ha muerto");

        // Activar la animaci�n de muerte
        animator.SetTrigger("Die");

        // Desactivar controles del jugador (si tienes un script de movimiento, desact�valo)
        GetComponent<PlayerController>().enabled = false;

        // Mostrar el MissionFailedCanvas despu�s de la animaci�n
        StartCoroutine(ShowMissionFailedCanvas());
    }

    private IEnumerator ShowMissionFailedCanvas()
    {
        yield return new WaitForSeconds(2f); // Espera que la animaci�n termine
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

        // Limitar salud al m�ximo
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        // Actualizar UI
        UpdateHealthUI();
        Debug.Log("Jugador recupera salud. Salud actual: " + currentHealth);
    }

    private void UpdateHealthUI()
    {
        // Activar o desactivar corazones seg�n la salud actual
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true; // Mostrar coraz�n
            }
            else
            {
                hearts[i].enabled = false; // Ocultar coraz�n
            }
        }
    }
}
