using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{
    private Rigidbody2D rb;
    //private Collider2D coll;
    public Transform top, bottom;
    public float Speed;
    public float TopY, BottomY;
    private bool isUp;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //coll= GetComponent<Collider2D>();
        TopY = top.transform.position.y;
        BottomY= bottom.transform.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    void Update()
    {
        Movement();
    }
    void Movement()
    {
        if (isUp) 
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if(transform.position.y > TopY)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if(transform.position.y < BottomY)
            {
                isUp = true;
            }
        }
    }
}
