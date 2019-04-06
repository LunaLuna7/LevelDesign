using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(LayerMask.NameToLayer("Ground")))
            Destroy(gameObject);
        
    }
}
