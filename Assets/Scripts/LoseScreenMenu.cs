 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenMenu : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Scenes/Start");
        Time.timeScale = 1f;
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
