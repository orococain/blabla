using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject mainMenu;
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Start");
    }
    
    public void OpenOption()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame");
    }
}
