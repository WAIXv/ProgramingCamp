using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public float speed;
    float xVelocity;
    public float jumpforce=5;
    public LayerMask ground;
    public Collider2D coll;
    public int Cherry;
    private bool isHurt;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
    }

    void Movement()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xVelocity *speed, rb.velocity.y);
        anim.SetFloat("running", Mathf.Abs(xVelocity));
        //角色移动
        if(xVelocity != 0)
        {
            transform.localScale = new Vector3(xVelocity, 1, 1);
        }

        //角色跳跃
        if(Input.GetButtonDown("Jump")&& coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(xVelocity, jumpforce);
            anim.SetBool("jumping", true);
        }
        
    }
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if (isHurt)
        {
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }
    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Cherry += 1;
        }
    }
    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            if (anim.GetBool("falling"))
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(xVelocity, jumpforce);
                anim.SetBool("jumping", true);
            }else if(transform.position.x<collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
    }
}
