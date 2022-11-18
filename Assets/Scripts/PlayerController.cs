using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D feet;
    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Run();
        CheckGrounded();
        Jump();
        SwitchAnim();
    }

    void Flip()//水平翻转
    {
        bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;//x轴有速度
        if (playerHasXAxisSpeed)
        {
            if (rb.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);//默认不翻转
            }
            if (rb.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");//方向
        Vector2 playerVel = new Vector2(moveDir * runSpeed, rb.velocity.y);//速度
        rb.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;//x轴有速度
        anim.SetBool("Run", playerHasXAxisSpeed);//动画切换
    }
    void CheckGrounded()//检测是否接触地面
    {
        isGround = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            anim.SetBool("Jump", true);//播放动画
            Vector2 jumpVel = new Vector2(rb.velocity.x, jumpSpeed);//跳跃速度
            rb.velocity = jumpVel;
        }
    }

    void SwitchAnim()//动画切换
    {
        anim.SetBool("Idle", false);
        if (anim.GetBool("Jump"))//起跳
        {
            if (rb.velocity.y < 0.0f)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", true);
            }
        }
        else if (isGround)//落地
        {
            anim.SetBool("Fall", false);
            anim.SetBool("Idle", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//踩到敌人后起跳
    {
        if (anim.GetBool("Fall") && collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool("Jump", true);//播放动画
            Vector2 jumpVel = new Vector2(rb.velocity.x, jumpSpeed);//跳跃速度
            rb.velocity = jumpVel;
        }
    }
}
