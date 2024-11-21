using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winPanelController : MonoBehaviour
{
    public GameObject winPanel;  // El panel de "Win"

    private bool isGamePaused = false;  // Verificar si el juego está en pausa

    // Método para activar el panel y pausar el juego
    public void ShowWinPanel()
    {
        winPanel.SetActive(true);  // Mostrar el panel de "Win"
        PauseGame();  // Pausar el juego
    }

    // Método para el botón "Continuar"
    public void OnContinueButton()
    {
        ResumeGame();  // Reanudar el juego
        winPanel.SetActive(false);  // Ocultar el panel de "Win"
    }

    // Pausar el juego
    private void PauseGame()
    {
        if (!isGamePaused)
        {  // Pausar el tiempo
            isGamePaused = true;  // Cambiar el estado de pausa
        }
    }


    private void ResumeGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1f;  // Reanudar el tiempo
            isGamePaused = false;  // Cambiar el estado de pausa
        }
    }
}