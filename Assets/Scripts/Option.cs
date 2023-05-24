using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject mainMenu;
    
    public void GoBack()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ChangeVolume()
    {
        
    }
}
