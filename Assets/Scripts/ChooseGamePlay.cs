using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGamePlay : MonoBehaviour
{
    public GameObject buttonHolder;

    public GameObject menuChoose;

    private void Start()
    {
        menuChoose.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SwipeHold()
    {
        buttonHolder.SetActive(false);
        menuChoose.SetActive(false);
        Time.timeScale = 1f; 
    }

    public void ButtonHold()
    {
        buttonHolder.SetActive(true);
        Time.timeScale = 1f; 
        menuChoose.SetActive(false);
    }
}
