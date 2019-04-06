using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public float seedSpeed;
    public GameObject seed;
    public Transform seedSpawn;

    public float shootRate;
    public float startDelay = 0f;

    private void OnEnable()
    {
        InvokeRepeating("Shoot", startDelay, shootRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    public void Shoot()
    {
        GameObject projectile = Instantiate(seed, seedSpawn.position, seedSpawn.rotation);
        projectile.GetComponent<Projectile>().speed = seedSpeed;
    }

}
