using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gestiona las configuraciones del menú de opciones: gráficos, resolución, volumen y campo de visión (FOV).
/// Guarda y carga preferencias usando PlayerPrefs.
/// </summary>
public class OptionMenu : MonoBehaviour
{
    // Referencias a los elementos de UI
    public Dropdown graphicsDropdown, resolutionDropdown;
    public Slider volumeSlider, fovSlider;

    // Referencias a componentes del jugador y cámara
    public PlayerMovement player;
    public Camera playerCam;

    /// <summary>
    /// Se ejecuta al iniciar el script. Carga configuraciones desde PlayerPrefs o establece valores por defecto si es la primera vez.
    /// </summary>
    void Start()
    {
        // Si es la primera vez que se lanza el juego, se guardan valores por defecto
        if (!PlayerPrefs.HasKey("gameLaunched"))
        {
            PlayerPrefs.SetInt("graphics", 3); // Calidad gráfica media
            PlayerPrefs.SetInt("screenResolution", 0); // Resolución por defecto
            PlayerPrefs.SetFloat("masterVolume", 1); // Volumen al máximo
            PlayerPrefs.SetFloat("fieldOfView", 60); // FOV por defecto
            PlayerPrefs.SetInt("gameLaunched", 1); // Marca que el juego ya fue lanzado
            PlayerPrefs.Save();
        }

        // Cargar configuraciones guardadas en PlayerPrefs
        if (PlayerPrefs.HasKey("gameLaunched"))
        {
            // Calidad gráfica
            graphicsDropdown.value = PlayerPrefs.GetInt("graphics");
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("graphics"));

            // Resolución de pantalla (según índice)
            resolutionDropdown.value = PlayerPrefs.GetInt("screenResolution");
            int resIndex = PlayerPrefs.GetInt("screenResolution");

            if (resIndex == 0) Screen.SetResolution(1440, 900, true);
            if (resIndex == 1) Screen.SetResolution(3840, 2160, true);
            if (resIndex == 2) Screen.SetResolution(2560, 1440, true);
            if (resIndex == 3) Screen.SetResolution(1920, 1080, true);
            if (resIndex == 4) Screen.SetResolution(1280, 720, true);

            // Volumen maestro
            volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
            AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");

            // Campo de visión (FOV)
            fovSlider.value = PlayerPrefs.GetFloat("fieldOfView");
            playerCam.fieldOfView = PlayerPrefs.GetFloat("fieldOfView");
        }
    }

    /// <summary>
    /// Guarda y aplica la calidad gráfica seleccionada en el menú.
    /// </summary>
    public void setGraphics()
    {
        PlayerPrefs.SetInt("graphics", graphicsDropdown.value);
        PlayerPrefs.Save();
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("graphics"));
    }

    /// <summary>
    /// Guarda y aplica la resolución seleccionada en el menú.
    /// </summary>
    public void setResolution()
    {
        PlayerPrefs.SetInt("screenResolution", resolutionDropdown.value);
        PlayerPrefs.Save();

        int resIndex = PlayerPrefs.GetInt("screenResolution");

        if (resIndex == 0) Screen.SetResolution(1440, 900, true);
        if (resIndex == 1) Screen.SetResolution(3840, 2160, true);
        if (resIndex == 2) Screen.SetResolution(2560, 1440, true);
        if (resIndex == 3) Screen.SetResolution(1920, 1080, true);
        if (resIndex == 4) Screen.SetResolution(1280, 720, true);
    }

    /// <summary>
    /// Guarda y aplica el nuevo valor de campo de visión (FOV).
    /// </summary>
    public void setFOV()
    {
        PlayerPrefs.SetFloat("fieldOfView", fovSlider.value);
        PlayerPrefs.Save();
        playerCam.fieldOfView = PlayerPrefs.GetFloat("fieldOfView");
    }

    /// <summary>
    /// Guarda y aplica el nuevo valor del volumen maestro.
    /// </summary>
    public void setVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
        PlayerPrefs.Save();
        AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
    }
}
