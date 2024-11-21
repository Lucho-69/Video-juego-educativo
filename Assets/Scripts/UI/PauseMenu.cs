using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Configuraci�n de Pausa")]
    public GameObject menuPausa; // Panel o men� de pausa
    public KeyCode teclaPausa = KeyCode.Return; // Tecla para pausar/reanudar el juego

    private bool juegoPausado = false; // Estado de pausa del juego

    void Update()
    {
        // Verificar si se presion� la tecla de pausa
        if (Input.GetKeyDown(teclaPausa))
        {
            // Alternar entre pausar y reanudar
            if (juegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    // M�todo para pausar el juego
    void PausarJuego()
    {
        menuPausa.SetActive(true); // Mostrar el men� de pausa
        Time.timeScale = 0f; // Detener el tiempo del juego
        juegoPausado = true; // Cambiar el estado a pausado
        // Aqu� puedes a�adir l�gica adicional como detener sonidos o animaciones si es necesario
    }

    // M�todo para reanudar el juego
    public void ReanudarJuego()
    {
        menuPausa.SetActive(false); // Ocultar el men� de pausa
        Time.timeScale = 1f; // Reanudar el tiempo del juego
        juegoPausado = false; // Cambiar el estado a no pausado
        // Aqu� puedes reactivar sonidos o animaciones si es necesario
    }
}
