using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, optionsMenu;
    public PlayerMovement player;
    public bool paused;
    public string menuSceneName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused == true)
            {
                player.enabled = false;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                AudioListener.pause = true;
            }
            if (paused == false)
            {
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

    public void openOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void goBack()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    
    public void backToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("quit game");
    }


}