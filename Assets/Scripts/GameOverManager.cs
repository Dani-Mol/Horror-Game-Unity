using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Administra las acciones disponibles en la pantalla de Game Over,
/// como reiniciar el nivel o volver al menú principal.
/// </summary>
public class GameOverManager : MonoBehaviour
{
    /// <summary>
    /// Reinicia el nivel actual:
    /// - Restaura el tiempo del juego a la normalidad (en caso de que esté pausado).
    /// - Reinicia el contador de páginas recogidas.
    /// - Recarga la escena actual.
    /// </summary>
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // Reactiva el tiempo del juego (por si estaba pausado)
        pickupLetter.pagesCollected = 0; // Reinicia el contador de páginas
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Carga de nuevo la escena actual
    }

    /// <summary>
    /// Vuelve al menú principal:
    /// - Restaura el tiempo del juego.
    /// - Carga la escena llamada "Menu".
    /// </summary>
    public void VolverAlMenu()
    {
        Time.timeScale = 1f; // Asegura que el tiempo esté activo
        SceneManager.LoadScene("Menu"); // Carga la escena del menú principal
    }
}
