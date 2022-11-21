using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator Anim;
    private Collider2D Coll;

    public LayerMask Ground;
    public Transform leftpoint, rightpoint;
    public float Speed, JumpForce;
    private float leftx, rightx;

    private bool Faceleft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    
    void Update()
    {
        SwitchAnim();
    }

    void Movement()
    {
        if(Faceleft)//面向左侧
        {
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
            if (Faceleft)
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
        }
        else //面向右侧
        {
            
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
            if (!Faceleft)
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }
        }
    }

    void SwitchAnim()
    {
        if(Anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if(Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        Anim.SetTrigger("death");
    }
}
