using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class ComboCheck : MonoBehaviour
{

    public Image[] arrowImgs;
    public Sprite[] arrowSprites;
    public List<int> combo = new List<int>();
    public int maxComboLength = 10;
    public int correctIndex = 0;
    public float spawnDelay = 10f;

    void Start()
    {
        SpawnCombo();
    }

    void SpawnCombo()
    {
        // Disable unused images
        for (int i = combo.Count; i < maxComboLength; i++)
        {
            arrowImgs[i].enabled = false;
        }

        // Spawn a random int and set sprite for each arrowImg
        for (int i = 0; i < combo.Count; i++)
        {
            int randomInt = Random.Range(1, arrowSprites.Length); 
            arrowImgs[i].sprite = arrowSprites[randomInt];
            arrowImgs[i].enabled = true;
            combo.Add(randomInt);
        }
        StartCoroutine(StartSpawnDelay());
    }
     
    
    public void CheckInput(int input)
    {
        if (input == combo[correctIndex])
        {
            correctIndex++;
            if (correctIndex == combo.Count)
            {
                // Hoàn thành combo
                Debug.Log("Complete combo!");
                combo.Clear();
                SpawnCombo();
            }
        }
        else
        {
            combo.Clear();
            for (int i = 0; i < arrowImgs.Length; i++)
            {
                arrowImgs[i].enabled = false;
            }
            // Dừng combo và tạo combo mới
            Debug.Log("Incorrect input. Combo stopped.");
            SpawnCombo();
        }
    }
    
    IEnumerator StartSpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnCombo();
    }
}
  
  

