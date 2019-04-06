using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private GameManager gameManager;
   
    void Awake()
    {
        gameManager = GetComponentInParent<GameManager>();
    }

    //Triggers GameOver if player hits a DeadZone area on the game scene
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            gameManager.GameOver();
       
    }
}
