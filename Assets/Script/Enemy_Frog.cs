using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform leftpoint,rightpoint;

    public float Speed,JumpForce;

    private Collider2D coll;
    public LayerMask Ground;
    private float leftx,rightx;
    private bool Faceleft = true;
    

    private Animator Anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        if(Faceleft)
        {
            if(coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping",true);
                rb.velocity = new Vector2(-Speed,JumpForce);

            }
            
            if(transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1,1,1);
                Faceleft = false;
            }
        }
        else
        {
            if(coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping",true);
                rb.velocity = new Vector2(Speed,JumpForce);

            }
            if(transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1,1,1);
                Faceleft = true;
            }
        }
    }

    void SwitchAnim()
    {
        if(Anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0.1)
            {
                Anim.SetBool("jumping",false);
                Anim.SetBool("falling",true);
            }
        }
        if(coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling",false);
        }
    }
}
