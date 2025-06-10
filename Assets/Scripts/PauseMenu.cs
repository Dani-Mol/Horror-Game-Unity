using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla el comportamiento del menú de pausa dentro del juego, incluyendo pausa, reanudación, opciones, salida al menú principal o al escritorio.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;       // Menú de pausa principal
    public GameObject optionsMenu;     // Submenú de opciones dentro del menú de pausa
    public PlayerMovement player;      // Referencia al script de movimiento del jugador
    public bool paused;                // Estado actual del juego (pausado o no)
    public string menuSceneName;       // Nombre de la escena del menú principal

    /// <summary>
    /// Se ejecuta en cada frame. Detecta si el jugador presiona la tecla Escape para pausar o reanudar el juego.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused == true)
            {
                // Pausar el juego
                player.enabled = false;
                pauseMenu.SetActive(true);
                Time.timeScale = 0; // Congela el tiempo del juego
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                AudioListener.pause = true; // Pausa el audio globalmente
            }

            if (paused == false)
            {
                // Reanudar el juego
                player.enabled = true;
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                AudioListener.pause = false;
            }
        }
    }

    /// <summary>
    /// Reanuda el juego desde el menú de pausa.
    /// </summary>
    public void resumeGame()
    {
        player.enabled = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.pause = false;
        paused = false;
    }

    /// <summary>
    /// Abre el submenú de opciones desde el menú de pausa.
    /// </summary>
    public void openOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    /// <summary>
    /// Regresa del submenú de opciones al menú de pausa principal.
    /// </summary>
    public void goBack()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    /// <summary>
    /// Regresa al menú principal del juego.
    /// </summary>
    public void backToMenu()
    {
        // Asegúrate de que Time.timeScale esté en 1 para no dejar el juego congelado
        Time.timeScale = 1;
        SceneManager.LoadScene(menuSceneName);
    }

    /// <summary>
    /// Sale del juego (funciona solo en compilados, no en el editor).
    /// </summary>
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("quit game"); // Útil para pruebas en el editor
    }
}
