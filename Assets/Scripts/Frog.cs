using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frog : Enemy
{
    private Rigidbody2D rb;
    private Collider2D coll;
    public AudioSource jumpAudio;
    //private Animator Anim;
    public LayerMask Ground;
    public Transform leftpoint,rightpoint;
    private bool faceLeft = true;
    public float Speed, JumpFore;
    private float leftx, rightx;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //Anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }
    void Update()
    {
        SwitchAnim();
        //Movement();
    }
    void Movement()
    {
        if (faceLeft == true)
        {
            if(coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                if (transform.position.x != leftx) jumpAudio.Play();
                rb.velocity = new Vector2(-Speed, JumpFore);
            }
            if (transform.position.x < leftx)
            {  
                rb.velocity= new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                if (transform.position.x != rightx) jumpAudio.Play();
                rb.velocity = new Vector2(Speed, JumpFore);
            }
            if (transform.position.x > rightx)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }
    void SwitchAnim ()
    {
        if(Anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if(coll.IsTouchingLayers(Ground)&&Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
        }
    }
}
