using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttack : MonoBehaviour
{
public Image[] arrowImgs;
public Sprite[] arrowSprites;
public int aiLevel = 0;
public int comboLength = 5;
public float accuracy = 0.5f;
public float minWaitTime = 0.5f;
public float maxWaitTime = 2.0f;
public PlayerHealth playerHealth;
public EnemyHealth enemyHealth; 
public int damage = 5;  
private List<int> currentCombo;
private int currentArrowIndex;
public GameObject Hit;
public ParticleSystem punch;

public Image furyBar;                 // Reference to the UI image representing the fury bar
public int combosPerFury = 20;        // Number of combos required to fill the fury bar
public int furyDamage = 50;           // Damage to deal when fury is activated
private int combosCompleted = 0;      // Counter for number of combos completed
private bool furyReady = false;       // Flag indicating if fury is ready to be used

void Start()
{
    // Init current combo to an empty list
    currentCombo = new List<int>();

    // Hide all arrow images
    for (int i = 0; i < arrowImgs.Length; i++)
    {
        arrowImgs[i].gameObject.SetActive(false);
    }
    // Spawn first combo
    SpawnCombo();
}


void SpawnCombo()
{
    // Generate a new combo
    currentCombo.Clear();
    for (int i = 0; i < comboLength; i++)
    {
        currentCombo.Add(Random.Range(1, arrowSprites.Length));
    }

    // Update arrow images to match the new combo
    for (int i = 0; i < Mathf.Min(currentCombo.Count, arrowImgs.Length); i++)
    {
        arrowImgs[i].sprite = arrowSprites[currentCombo[i]];
        arrowImgs[i].gameObject.SetActive(true);
    }
    // Start executing the combo
    StartCoroutine(ExecuteCombo()); 
}

IEnumerator ExecuteCombo()
{
    for (int i = 0; i < currentCombo.Count; i++)
    {
        // Wait for a random amount of time based on the AI level
        float waitTime = Random.Range(minWaitTime, maxWaitTime) / (aiLevel + 1);
        yield return new WaitForSeconds(waitTime);

        // Determine if the AI hits the arrow based on accuracy
        bool hitArrow = Random.Range(0.0f, 1.0f) < accuracy;

        // If the AI hits the arrow, move on to the next arrow in the combo
        if (hitArrow)
        {
            arrowImgs[currentArrowIndex].color = Color.green;
            currentArrowIndex++;
            combosCompleted++;
            furyBar.fillAmount = Mathf.Clamp01((float)combosCompleted / combosPerFury); // Update fury bar
            if (combosCompleted >= combosPerFury) {
                furyReady = true;   // Fury is ready!
            }
            // Debug.Log("AI hit arrow " + i);
        }
        // If the AI misses the arrow, reset the combo and spawn a new one
        else
        {
            combosCompleted = 0;   // Reset combos completed
            furyReady = false;     // Fury is not ready anymore
            furyBar.fillAmount = 0;   // Reset fury bar
            arrowImgs[currentArrowIndex].color = Color.red;
            currentArrowIndex = 0;
            GameObject.Find("Enemy").GetComponent<Animator>().Play("Idle");
            // enemyHealth.TakeDamage(damage);
            SpawnCombo();
            // Debug.Log("AI missed arrow " + i);
            foreach (Image img in arrowImgs)
            {
                img.color = Color.white;
            }
            yield break;
        }

        // If the AI has successfully completed the entire combo, spawn a new one
        if (i == currentCombo.Count - 1)
        {
            punch.Play();
            currentArrowIndex = 0;
            playerHealth.TakeDamage(damage);
            if (Hit)
            {
                HitPlayer();
            }
            if (furyReady) {
                // Fury is ready, deal massive damage and reset all fury related variables
                enemyHealth.TakeDamage(furyDamage);
                combosCompleted = 0;
                furyReady = false;
                furyBar.fillAmount = 0;
            } else {
                GameObject.Find("Enemy").GetComponent<Animator>().Play("Attack");
            }
            foreach (Image img in arrowImgs)
            {
                img.color = Color.white;
            }
            SpawnCombo();
            yield break;
        }
    }
}

void HitPlayer()
{
    Instantiate(Hit,new Vector2(700, 1000) , Quaternion.identity, transform.parent);
}
}
