using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    // Start is called before the first frame update
    void Start()
    {
        instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ArrowHit()
    {
        Debug.Log(("HIT"));
    }

    public void ArrowMiss()
    {
        Debug.Log("Miss");
    }
}
