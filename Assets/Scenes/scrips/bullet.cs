using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speedx;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right *speedx;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
