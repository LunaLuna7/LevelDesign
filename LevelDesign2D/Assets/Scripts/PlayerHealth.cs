using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public GameManager gameManager;

    


    // Update is called once per frame
    void Update()
    {
        
    }


    public void DamagePlayer(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            gameManager.GameOver();
        }
    }
}
