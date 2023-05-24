
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    public float maxHealth = 100;
    public float currentHealth;
    public Slider healthBar;
    public GameObject loseMenu;
    public TMP_Text textBox;
    public GameObject floatingText;
    void Start()
    {
        healthBar.value = currentHealth; 
        currentHealth = maxHealth;
        textBox.text = "Health: " + currentHealth;
    }
    
    

    public void TakeDamage(float  amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            StartCoroutine(ShowGameOverScreen());
            Time.timeScale = 1f;
            GameObject.Find("Player").GetComponent<Animator>().Play("Death");
            GameObject.Find("Enemy").GetComponent<Animator>().Play("Victory");
        }
        healthBar.value = currentHealth; 
        textBox.text = "Health: " + currentHealth;
        if (floatingText)
        {
            showFlowingtext();
        }
    }

    void showFlowingtext()
    {
        Instantiate(floatingText, new Vector2(900, 1000), Quaternion.identity, transform.parent);
    }
    
    private IEnumerator ShowGameOverScreen ()
    {
        yield return new WaitForSeconds (2);
        Time.timeScale = 0;
        loseMenu.SetActive (true);
        
    }
}
