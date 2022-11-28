using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_frog :enemy
{
    [SerializeField] private Rigidbody2D Rb;
    //[SerializeField] private Animator Anim;
    [SerializeField] private Collider2D Coll;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float Speed,leftx,rightx;
    [SerializeField] private float Jumpforce;
    [SerializeField] private Transform leftpoint, rightpoint;
    
    [SerializeField] private bool Faceleft = true;
    
        protected override void Start()
    {
        base.Start();
       // Anim = GetComponent<Animator>(); 
        Rb = GetComponent<Rigidbody2D>();
        Coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        rightx = rightpoint.position.x;
        leftx = leftpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    
    void Update()
    {
        
         Switchanim();
    }
    void Movement()
    {
        if (Faceleft)
        {   
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                Rb.velocity = new Vector2(-Speed, Jumpforce);
            }
        }
        else
        {
           
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }          
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                Rb.velocity = new Vector2(Speed, Jumpforce);
            }
        }
        

    }


    void Switchanim()
    {
        if (Anim.GetBool("jumping"))
        {
            if (Rb.velocity.y < 0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if (Coll.IsTouchingLayers(Ground))
        {
            Anim.SetBool("falling", false);
        }
    }
   
}
   

