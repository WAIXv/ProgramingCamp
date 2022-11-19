using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator anim;
    private Collider2D coll;
    public LayerMask ground;    //地面图层
    public Text score;
    public Collider2D disColl;
    public Transform cellingCheck;

    public float speed;         //速度变量
    public float JumpForce;     //跳跃速度
    private int cherry = 0;          //吃掉的樱桃数量
    private bool CanJump = true;        //用于判断能否跳跃
    private bool isHurt = false;        //判断是否受伤
    private bool isPast = false;        //时空穿越 判断是否处在过去
    
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
        if(!isHurt)
        {
            Movement();
        }
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
            if (HorizontalSpeed < 0)                         //人物向左转向
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (HorizontalSpeed > 0)                         //人物向右转向
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            rd.velocity = new Vector2(0, rd.velocity.y);
        }
        if (Input.GetButtonDown("Jump") && CanJump)                 //跳跃
        {
            rd.velocity = new Vector2(rd.velocity.x, JumpForce);
            anim.SetBool("jumping", true);
            CanJump = false;
        }
        if (!isPast)
        {
            if (Input.GetKeyDown("q"))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 51);
                isPast = true;
            }
        }
        else if (isPast)
        {
            if (Input.GetKeyDown("q"))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 51);
                isPast = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))              //按下ctrl键
        {
            JumpForce *= 0.8f;                                  //减少跳跃高度
        }
        if (!Physics2D.OverlapCircle(cellingCheck.position,0.2f,ground)) 
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                anim.SetBool("crouch", true);                       //切换蹲下动画  存在bug：保持蹲下状态原地跳 落地后无法再次跳跃 除非让人物去触碰墙体才能再次跳跃
                rd.velocity = new Vector2(0.5f * HorizontalSpeed, rd.velocity.y);
                disColl.enabled = false;                            //关闭上半身碰撞体
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                anim.SetBool("crouch", false);                      //切出蹲下动画
                JumpForce *= 1.25f;
                disColl.enabled = true;                             //打开上半身碰撞体
            }
        }

    }
        void SwitchAnimation()
     {
             if (anim.GetBool("jumping") && rd.velocity.y < 0)       //下落
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
                CanJump = false;
            }
            else if (anim.GetBool("falling") && coll.IsTouchingLayers(ground))         //落地
            {
                anim.SetBool("falling", false);
                anim.SetBool("idle", true);
                CanJump = true;
            }
            if (isHurt && Mathf.Abs(rd.velocity.x) < 1)     //结束受伤动画
            {
                isHurt = false;
                anim.SetBool("hurt", false);
            }      
    }
    private void OnTriggerEnter2D(Collider2D collision)             
    {
        if (collision.gameObject.tag == "Collection")               //当角色触碰到物品
        {
            Destroy(collision.gameObject);                          //吃掉物品
            cherry++;
            score.text = cherry.ToString();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)      
    {
        if(collision.gameObject.tag == "Enemy")             //存在问题：受伤时候玩家受伤动画只播放一遍  已解决：any state的动画流向自身设置 被禁用 导致只播放一遍
        {
            if(anim.GetBool("falling"))         //下落状态
            {
                Destroy(collision.gameObject);      //消灭敌人
                rd.velocity = new Vector2(rd.velocity.x, JumpForce);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                isHurt = true;
                anim.SetBool("hurt", true);
                rd.velocity = new Vector2(-6, rd.velocity.y);
            }
            else if(transform.position.x > collision.gameObject.transform.position.x)
            {
                isHurt = true;
                anim.SetBool("hurt", true);
                rd.velocity = new Vector2(6, rd.velocity.y);
            }
        }
    }
}
