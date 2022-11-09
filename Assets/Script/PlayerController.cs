using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rd;
    public Animator anim;
    public Collider2D coll;


    public float speed;         //速度变量
    public float JumpForce;     //跳跃速度
    public LayerMask Ground;    //地面图层

    private bool CanJump = true;        //用于判断能否跳跃
    
    
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();   
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnimation();
    }

    void Movement()
    {
        float HorizontalSpeed = Input.GetAxis("Horizontal") * speed;        //不乘以Time.deltaTime反而不会瞬移？
        float FacedDirection = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("running", Mathf.Abs(FacedDirection));
        if (FacedDirection != 0)                   //水平移动
        {
            rd.velocity = new Vector2(HorizontalSpeed, rd.velocity.y);
            if(HorizontalSpeed < 0)                         //人物向左转向
            {
                transform.localScale = new Vector3(-1,1,1);
            }
            if (HorizontalSpeed > 0)                         //人物向右转向
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            rd.velocity = new Vector2(0,rd.velocity.y);
        }
        if(Input.GetButtonDown("Jump") && CanJump)                 //跳跃
        {
            rd.velocity = new Vector2(rd.velocity.x,JumpForce);
            anim.SetBool("jumping",true);
            CanJump = false;
        }
    }
    void SwitchAnimation()
    {
        if(anim.GetBool("jumping"))
        {
                    if (rd.velocity.y < 0)                     //下落
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
                CanJump = false;
            }
        }
        else if (coll.IsTouchingLayers(Ground))         //落地
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
            if (rd.velocity.y == 0)                      //解决无限跳问题
            {
                CanJump = true;
            }
        }
    }
}
