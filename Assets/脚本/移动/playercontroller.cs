using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    [SerializeField] private AudioSource touchAudio;//碰撞时果实时的音效
    [SerializeField] private AudioSource jumpAudio;//跳跃时的音效
    [SerializeField] private int acornNum;//果实数量
    [SerializeField] private float speed;//走路速度
    [SerializeField] private float jumpSpeed;//跳跃速度
    [SerializeField] private Text acorn;
    private Rigidbody2D myRigidbody;//获取玩家刚体
    private Animator myAnime;//获取动画机
    private BoxCollider2D myFeet;//获取碰撞机
    private bool isGround;//判断是否接触地面的信息
    private bool canDoubleJump;//判断能否二段跳
    private bool isHurt;//判断是否受伤
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnime = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        if (!isHurt)
        {
            run();
        }
        flip();
        jump();
        SwitchAnimation();

    }
    /// <summary>
    /// 判断是否接触地面
    /// </summary>
    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    /// <summary>
    /// 转向
    /// </summary>
    void flip()
    {
        bool playerAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    /// <summary>
    /// 跑
    /// </summary>
    void run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * speed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool playerAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnime.SetBool("run", playerAxisSpeed);
    }
    /// <summary>
    /// 跳
    /// </summary>
    void jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnime.SetBool("jump", true);
                canDoubleJump = true;
                jumpAudio.Play();
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
            }
            else if (canDoubleJump)
            {
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                jumpAudio.Play();
                canDoubleJump = false;
            }


        }
    }
    /// <summary>
    /// 动画切换
    /// </summary>
    void SwitchAnimation()
    {
        if (myAnime.GetBool("jump"))
        {
            if (myRigidbody.velocity.y == 0.0f)
            {
                myAnime.SetBool("jump", false);
            }
        }
        if (isHurt)
        {
            if (Mathf.Abs(myRigidbody.velocity.x) < 2f)
            {
                isHurt = false;
                myAnime.SetBool("hurt", false);
            }
        }
    }
    /// <summary>
    /// 判断是否与果实相撞
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "healthPack")
        {
            touchAudio.Play();
            Destroy(collision.gameObject);
            acornNum += 1;
            acorn.text = acornNum.ToString();

        }
    }
    /// <summary>
    /// 判断是否与敌人相撞
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ant")
        {
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                myRigidbody.velocity = new Vector2(-10, myRigidbody.velocity.y);
                isHurt = true;
                myAnime.SetBool("hurt", true);
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                myRigidbody.velocity = new Vector2(10, myRigidbody.velocity.y);
                isHurt = true;
                myAnime.SetBool("hurt", true);
            }
        }
    }
}


