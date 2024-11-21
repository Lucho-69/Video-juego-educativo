using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Configuración de Pausa")]
    public GameObject menuPausa; // Panel o menú de pausa
    public KeyCode teclaPausa = KeyCode.Return; // Tecla para pausar/reanudar el juego

    private bool juegoPausado = false; // Estado de pausa del juego

    void Update()
    {
        // Verificar si se presionó la tecla de pausa
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

    // Método para pausar el juego
    void PausarJuego()
    {
        menuPausa.SetActive(true); // Mostrar el menú de pausa
        Time.timeScale = 0f; // Detener el tiempo del juego
        juegoPausado = true; // Cambiar el estado a pausado
        // Aquí puedes añadir lógica adicional como detener sonidos o animaciones si es necesario
    }

    // Método para reanudar el juego
    public void ReanudarJuego()
    {
        menuPausa.SetActive(false); // Ocultar el menú de pausa
        Time.timeScale = 1f; // Reanudar el tiempo del juego
        juegoPausado = false; // Cambiar el estado a no pausado
        // Aquí puedes reactivar sonidos o animaciones si es necesario
    }
}
