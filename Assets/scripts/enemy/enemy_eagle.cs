using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_eagle : enemy

{
    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private Animator Anim;
    [SerializeField] private float Speed, leftx, rightx;
    [SerializeField] private Transform leftpoint, rightpoint;

    [SerializeField] private bool Faceleft = true;

     protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        transform.DetachChildren();
        rightx = rightpoint.position.x;
        leftx = leftpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }
   
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        if (Faceleft)
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }


    }
    

}
