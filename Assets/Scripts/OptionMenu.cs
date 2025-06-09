using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public Dropdown graphicsDropdown, resolutionDropdown;
    public Slider volumeSlider, fovSlider;
    public PlayerMovement player;
    public Camera playerCam;

    void Start()
    {
        if (!PlayerPrefs.HasKey("gameLaunched"))
        {
            PlayerPrefs.SetInt("graphics", 3); // 3 = Very High, o el valor correcto
            PlayerPrefs.SetInt("screenResolution", 0);
            PlayerPrefs.SetFloat("masterVolume", 1);
            PlayerPrefs.SetFloat("fieldOfView", 60); // Como dijiste antes
            PlayerPrefs.SetInt("gameLaunched", 1); // ✅ Esto indica que ya se inicializó
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("gameLaunched"))
        {
            graphicsDropdown.value = PlayerPrefs.GetInt("graphics");
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("graphics"));

            resolutionDropdown.value = PlayerPrefs.GetInt("screenResolution");
            if(PlayerPrefs.GetInt("screenResolution", 0) == 0)
            {
                Screen.SetResolution(1440, 900, true);
            }
            if (PlayerPrefs.GetInt("screenResolution", 1) == 1)
            {
                Screen.SetResolution(3840, 2160, true);
            }
            if (PlayerPrefs.GetInt("screenResolution", 2) == 2)
            {
                Screen.SetResolution(2560, 1440, true);
            }
            if (PlayerPrefs.GetInt("screenResolution", 3) == 3)
            {
                Screen.SetResolution(1920, 1080, true);
            }
            if (PlayerPrefs.GetInt("screenResolution", 4) == 4)
            {
                Screen.SetResolution(1280, 720, true);
            }

            volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
            AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");

            fovSlider.value = PlayerPrefs.GetFloat("fieldOfView");
            playerCam.fieldOfView = PlayerPrefs.GetFloat("fieldOfView");
        }
    }
    public void setGraphics()
    {
        PlayerPrefs.SetInt("graphics", graphicsDropdown.value);
        PlayerPrefs.Save();
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("graphics"));
    }
    public void setResolution()
    {
        PlayerPrefs.SetInt("screenResolution", resolutionDropdown.value);
        PlayerPrefs.Save();
        if (PlayerPrefs.GetInt("screenResolution", 0) == 0)
        {
            Screen.SetResolution(1440, 900, true);
        }
        if (PlayerPrefs.GetInt("screenResolution", 1) == 1)
        {
            Screen.SetResolution(3840, 2160, true);
        }
        if (PlayerPrefs.GetInt("screenResolution", 2) == 2)
        {
            Screen.SetResolution(2560, 1440, true);
        }
        if (PlayerPrefs.GetInt("screenResolution", 3) == 3)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        if (PlayerPrefs.GetInt("screenResolution", 4) == 4)
        {
            Screen.SetResolution(1280, 720, true);
        }
    }
    public void setFOV()
    {
        PlayerPrefs.SetFloat("fieldOfView", fovSlider.value);
        PlayerPrefs.Save();
        playerCam.fieldOfView = PlayerPrefs.GetFloat("fieldOfView");
    }
    public void setVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", volumeSlider.value);
        PlayerPrefs.Save();
        AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
    }
    
}