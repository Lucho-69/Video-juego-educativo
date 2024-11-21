using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Salud máxima
    private int currentHealth; // Salud actual

    public Image[] hearts; // Array de imágenes de corazones
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

        // Verificar si el jugador murió
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        // Actualizar UI
        UpdateHealthUI();
        Debug.Log("Jugador recibe daño. Salud actual: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("Jugador ha muerto");

        // Activar la animación de muerte
        animator.SetTrigger("Die");

        // Desactivar controles del jugador (si tienes un script de movimiento, desactívalo)
        GetComponent<PlayerController>().enabled = false;

        // Mostrar el MissionFailedCanvas después de la animación
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

        // Limitar salud al máximo
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        // Actualizar UI
        UpdateHealthUI();
        Debug.Log("Jugador recupera salud. Salud actual: " + currentHealth);
    }

    private void UpdateHealthUI()
    {
        // Activar o desactivar corazones según la salud actual
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].enabled = true; // Mostrar corazón
            }
            else
            {
                hearts[i].enabled = false; // Ocultar corazón
            }
        }
    }
}
