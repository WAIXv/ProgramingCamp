using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D coll;
    public Transform left, right;
    public LayerMask ground;

    public float jumpForce,speed;
    private bool isFaceLeft = true;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        transform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        FrogMove();
    }
    void FrogJump()                 //青蛙跳跃
    {
        float horizontalSpeed = speed;
        float verticalSpeed = jumpForce;
        if (transform.position.x < left.position.x)         //青蛙右转
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFaceLeft = false;
        }
        else if (transform.position.x > right.position.x)     //青蛙左转
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFaceLeft = true;
        }
        if (isFaceLeft)                                     //青蛙向左跳
        {
            rb.velocity = new Vector2(-horizontalSpeed, verticalSpeed);
            anim.SetBool("jumping", true);
        }
        else                                                //青蛙向右跳
        {
            rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
            anim.SetBool("jumping", true);
        }
    }
    void FrogMove()                 //青蛙动画转换
    {
        if (rb.velocity.y < 0)
        {
            anim.SetBool("falling", true);
            anim.SetBool("jumping", false);
        }
        if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
        }
    }
}
