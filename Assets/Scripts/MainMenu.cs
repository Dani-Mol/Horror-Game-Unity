using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador del menú principal. Gestiona navegación entre paneles,
/// selección de dificultad, inicio del juego y salida de la aplicación.
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Referencias a los diferentes paneles del menú
    public GameObject menu;       // Menú principal
    public GameObject options;    // Panel de opciones
    public GameObject credits;    // Panel de créditos
    public GameObject difficulty; // Panel de selección de dificultad
    public GameObject loading;    // Pantalla de carga

    // Nombre de la escena del juego (puede no estar usado directamente aquí)
    public string gameSceneName;

    // Música de fondo del menú
    public AudioSource menuMusic;

    /// <summary>
    /// Reproduce la música del menú al iniciar la escena.
    /// </summary>
    void Start()
    {
        menuMusic.Play();
    }

    /// <summary>
    /// Abre el panel de opciones y oculta el menú principal.
    /// </summary>
    public void openOptions()
    {
        menu.SetActive(false);
        options.SetActive(true);
    }

    /// <summary>
    /// Abre el panel de créditos y oculta el menú principal.
    /// </summary>
    public void openCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    /// <summary>
    /// Vuelve desde cualquier submenú al menú principal.
    /// </summary>
    public void goBack()
    {
        options.SetActive(false);
        credits.SetActive(false);
        difficulty.SetActive(false);
        menu.SetActive(true);
    }

    /// <summary>
    /// Cierra la aplicación. Solo funciona fuera del editor.
    /// </summary>
    public void quitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Abre el panel de selección de dificultad y oculta el menú principal.
    /// </summary>
    public void playGame()
    {
        menu.SetActive(false);
        difficulty.SetActive(true);
    }

    /// <summary>
    /// Inicia el juego en modo difícil:
    /// - Guarda la dificultad como 0 en PlayerPrefs.
    /// - Reinicia el contador de páginas.
    /// - Carga la escena "Scene 1".
    /// </summary>
    public void Hard()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (menuMusic.isPlaying) menuMusic.Stop();

        PlayerPrefs.SetInt("difficulty", 0); // 0 = Hard
        PlayerPrefs.Save();

        pickupLetter.pagesCollected = 0;
        difficulty.SetActive(false);
        loading.SetActive(true);

        SceneManager.LoadScene("Scene 1");
    }

    /// <summary>
    /// Inicia el juego en modo medio:
    /// - Guarda la dificultad como 1 en PlayerPrefs.
    /// - Carga la escena "Scene 2".
    /// </summary>
    public void Medium()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (menuMusic.isPlaying) menuMusic.Stop();

        PlayerPrefs.SetInt("difficulty", 1); // 1 = Medium
        PlayerPrefs.Save();

        pickupLetter.pagesCollected = 0;
        difficulty.SetActive(false);
        loading.SetActive(true);

        SceneManager.LoadScene("Scene 2");
    }

    /// <summary>
    /// Inicia el juego en modo fácil:
    /// - Guarda la dificultad como 2 en PlayerPrefs.
    /// - Carga la escena "Scene 3".
    /// </summary>
    public void Easy()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (menuMusic.isPlaying) menuMusic.Stop();

        PlayerPrefs.SetInt("difficulty", 2); // 2 = Easy
        PlayerPrefs.Save();

        pickupLetter.pagesCollected = 0;
        difficulty.SetActive(false);
        loading.SetActive(true);

        SceneManager.LoadScene("Scene 3");
    }
}
