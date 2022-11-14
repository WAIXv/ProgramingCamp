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
    void FrogJump()                 //������Ծ
    {
        float horizontalSpeed = speed;
        float verticalSpeed = jumpForce;
        if (transform.position.x < left.position.x)         //������ת
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFaceLeft = false;
        }
        else if (transform.position.x > right.position.x)     //������ת
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFaceLeft = true;
        }
        if (isFaceLeft)                                     //����������
        {
            rb.velocity = new Vector2(-horizontalSpeed, verticalSpeed);
            anim.SetBool("jumping", true);
        }
        else                                                //����������
        {
            rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
            anim.SetBool("jumping", true);
        }
    }
    void FrogMove()                 //���ܶ���ת��
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
