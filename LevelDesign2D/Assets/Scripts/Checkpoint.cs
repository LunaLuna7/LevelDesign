using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    //When the player touches a check point, it updates the spawn position for the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            GameManager.SpawnPosition = gameObject.transform;
    }
}
