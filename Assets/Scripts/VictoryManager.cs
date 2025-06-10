using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gestiona las acciones tras la victoria, como volver al menú, reiniciar el nivel o salir del juego.
/// </summary>
public class VictoryManager : MonoBehaviour
{
    /// <summary>
    /// Vuelve al menú principal. También asegura que el tiempo de juego esté normalizado.
    /// </summary>
    public void backToMenu()
    {
        Time.timeScale = 1f; // Reinicia el tiempo del juego a velocidad normal
        SceneManager.LoadScene("Menu"); // Carga la escena del menú principal
    }

    /// <summary>
    /// Reinicia la escena actual para jugar de nuevo. Resetea las páginas recogidas y normaliza el tiempo.
    /// </summary>
    public void playAgain()
    {
        Time.timeScale = 1f; // Reinicia el tiempo del juego a velocidad normal
        pickupLetter.pagesCollected = 0; // Resetea el contador de páginas recogidas
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual
    }

    /// <summary>
    /// Sale de la aplicación.
    /// </summary>
    public void quitGame()
    {
        Application.Quit(); // Cierra la aplicación
    }
}
