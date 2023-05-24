using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.VFX;
using Slider = UnityEngine.UIElements.Slider;
using TMPro;

public class SpawnArrow : MonoBehaviour
{
    [SerializeField]
    public Image[] arrowImgs;
    public Sprite[] arrowSprites;
    public List<int> combo = new List<int>();
    private int maxArrows = 9;
    public PlayerHealth playerHealth;
    public int damage = 5;
    public EnemyHealth enemyHealth;
    private int currentArrowIndex;
    public int damagePerSwipe = 2; 
    public GameObject floatingPerfect;
    public screenShake failed;
    public float shake;
    public ParticleSystem punch;
    public Image furyBar;     // Reference to the UI image representing the fury bar
    public int combosPerFury = 20;  // Number of combos required to fill the fury bar
    public int furyDamage = 50;     // Damage to deal when fury is activated
    private int combosCompleted ; // Counter for number of combos completed
    public TMP_Text textBox;
    public GameObject floatingDamageRage;
    private int comboCount ;
    public TMP_Text comboCountText; // Number of combos completed
    public GameObject comboText;
    public AudioSource correctClick;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn initial combo
        SpawnCombo();
    }

    // Update is called once per frame
   public  void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            // Calculate swipe direction
            Vector2 swipeDelta = Input.GetTouch(0).deltaPosition;
            swipeDelta.Normalize();
            int input = -1;

            // Determine swipe direction and call corresponding input
            if (swipeDelta.y > 0.5f && Mathf.Abs(swipeDelta.x) < 0.5f)
            {
                input = 2; // Up swipe
            }
            else if (swipeDelta.y < -0.5f && Mathf.Abs(swipeDelta.x) < 0.5f)
            {
                input = 3; // Down swipe
            }
            else if (swipeDelta.x > 0.5f && Mathf.Abs(swipeDelta.y) < 0.5f)
            {
                input = 0; // Right swipe
            }
            else if (swipeDelta.x < -0.5f && Mathf.Abs(swipeDelta.y) < 0.5f)
            {
                input = 1; // Left swipe
            }

            if (input != -1)
            {
                CheckInput(input);
            }
        }
        UpdateComboCountText();
    }

    // Spawn a new combo
    void SpawnCombo()
    {
        combo.Clear();

        // Randomly generate combo of arrows
        // Generate new combo only if current combo is complete
        if (combo.Count == 0)
        {
            // Randomly generate combo of arrows
            int comboLength = Random.Range(4, maxArrows + 1);
            for (int i = 0; i < comboLength; i++)
            {
                int arrowIndex = Random.Range(0, arrowSprites.Length);
                combo.Add(arrowIndex);
                // Set sprite for arrow image
                if (i < arrowImgs.Length)
                {
                    arrowImgs[i].enabled = true;
                    arrowImgs[i].sprite = arrowSprites[arrowIndex];
                }
            }

            // Disable unused arrow images
            for (int i = comboLength; i < arrowImgs.Length; i++)
            {
                arrowImgs[i].enabled = false;
            }
        }
    }

    // Check input for current arrow direction
    public void CheckInput(int input)
    {
        {
           if (combo.Count > 0 && combo[0] == input ) 
           {
               // Correct arrow input
               combo.RemoveAt(0);
               arrowImgs[0].enabled = true;
               arrowImgs[currentArrowIndex].color = Color.green;
               currentArrowIndex++;
               GameObject.Find("Arrow").GetComponent<Animator>().Play("arrowAnimator");
               correctClick.Play();
               // Increment combo counter
               combosCompleted++;
               // Change color of arrow images
               if (combo.Count == 0)
               {
                   comboCount++;
                   punch.Play();
                   foreach(Image img in arrowImgs)
                   {
                       img.color = Color.white;
                   }
       
                   // Combo successful, spawn new combo
                   SpawnCombo();
                   enemyHealth.TakeDamage(damage);
                   currentArrowIndex = 0;
                   GameObject.Find("Player").GetComponent<Animator>().Play("Attack");
                   // Increment fury points by 5 for each combo completed
                   float furyPoints = Mathf.Min(furyBar.fillAmount * 100 + 5, 100);
                   furyBar.fillAmount = furyPoints / 100f;
                   textBox.text =   furyPoints+ " /100 ";
                   // Check if fury is activated
                   if (furyPoints == 100 )
                   { 
                       if (floatingDamageRage)
                       {
                           showFlowingRage();
                       }
                       // Deallocate fury points
                       furyBar.fillAmount = 0f;
                       combosCompleted = 0;
                       // Deal massive damage to enemy
                       GameObject.Find("Player").GetComponent<Animator>().Play("HeavyAttack");
                       enemyHealth.TakeDamage(furyDamage);
                   }
                   if (floatingPerfect)
                   {
                       showFlowingtext();
                   }
                   comboText.SetActive(true);
               }
           }
           else
           {
               // Incorrect arrow input
               failed.shake(shake);
               GameObject.Find("Player").GetComponent<Animator>().Play("Idle");
               arrowImgs[currentArrowIndex].color = Color.red;
               GameOver();
           }
       }
    }
    
    public void GameOver() 
    {
        comboCount = 0;
        comboText.SetActive(false);
    }
    void showFlowingtext()
    { 
        Instantiate(floatingPerfect,new Vector2(1500, 1000) , Quaternion.identity, transform.parent);
    }

    void showFlowingRage()
    {
        Instantiate(floatingDamageRage, new Vector2(1400, 1000), Quaternion.identity, transform.parent);
    }
 
    void UpdateComboCountText()
    {
        comboCountText.text = "Combos Completed: " + comboCount;
    }
}

 