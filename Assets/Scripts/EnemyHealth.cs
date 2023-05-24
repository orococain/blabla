

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Quaternion = UnityEngine.Quaternion;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    public float  health;

    public float maxHealth = 100f;

    public Slider healthBar;

    public GameObject winScreen;
    
    public TMP_Text textBox; 
    
    public GameObject floatingText;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = health;
        textBox.text = "Health: " + health;
        health = maxHealth;
    }
    
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            StartCoroutine(ShowGameOverScreen());
            Time.timeScale = 1f;
            GameObject.Find("Enemy").GetComponent<Animator>().Play("Death");
            GameObject.Find("Player").GetComponent<Animator>().Play("Victory");
        }
        healthBar.value = health;
        textBox.text = "Health: " + health;
        if (floatingText)
        {
            showFlowingtext();
        }
        
    }
    
    void showFlowingtext()
    { 
        Instantiate(floatingText,new Vector2(1500, 1000) , Quaternion.identity, transform.parent);
    }

    private IEnumerator  ShowGameOverScreen ()
    {
        yield return new WaitForSeconds (2);
        Time.timeScale = 0;
        winScreen.SetActive (true);
    }
    
}

