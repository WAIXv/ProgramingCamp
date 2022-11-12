using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stair : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speedx;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right *speedx;
    }
}
