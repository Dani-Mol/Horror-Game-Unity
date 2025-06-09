using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu, options, credits, difficulty, loading;
    public string gameSceneName;

    public AudioSource menuMusic;

    void Start()
    {   
            menuMusic.Play();
    }


    public void openOptions()
    {
        menu.SetActive(false);
        options.SetActive(true);
    }

    public void openCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void goBack()
    {
        options.SetActive(false);
        credits.SetActive(false);
        difficulty.SetActive(false);
        menu.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void playGame()
    {
        menu.SetActive(false);
        difficulty.SetActive(true);
    }

    public void Hard()
    {
        Time.timeScale = 1f;  
        AudioListener.pause = false;  
        if (menuMusic.isPlaying) menuMusic.Stop();
        PlayerPrefs.SetInt("difficulty", 0);
        PlayerPrefs.Save();
        pickupLetter.pagesCollected = 0;
        difficulty.SetActive(false);
        loading.SetActive(true);
        SceneManager.LoadScene("Scene 1");
    }

    public void Medium()
    {   
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (menuMusic.isPlaying) menuMusic.Stop();
        PlayerPrefs.SetInt("difficulty", 1);
        PlayerPrefs.Save();
        pickupLetter.pagesCollected = 0;
        difficulty.SetActive(false);
        loading.SetActive(true);
        SceneManager.LoadScene("Scene 2");
    }

    public void Easy()
    {   
        Time.timeScale = 1f;
        AudioListener.pause = false;
        if (menuMusic.isPlaying) menuMusic.Stop();
        PlayerPrefs.SetInt("difficulty", 2);
        PlayerPrefs.Save();
        pickupLetter.pagesCollected = 0;
        difficulty.SetActive(false);
        loading.SetActive(true);
        SceneManager.LoadScene("Scene 3");
    }

}
