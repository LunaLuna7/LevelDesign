using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player; //The player GameObject on the scene
    static public Transform SpawnPosition; //The location that the player will spawn

    //Moves the player to the SPawnPosition and Calls playerHealth Healing function
    public void GameOver()
    {
        StartCoroutine(RestartGame());
    }

    //Restarts Players health and position with a .5 second delay
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(.5f);
        player.transform.position = SpawnPosition.position;
    }
}
