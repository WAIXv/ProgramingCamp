using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrler : MonoBehaviour
{
    #region 声明部分
    [Header("土狼时间/延迟检测地面")]
    [SerializeField] private float ycJumpTemp;                      //延迟地面检测参考值
    private float ycJump;

    [Header("移动与多段跳")]
    [SerializeField] private float Face;
    [SerializeField] private float ySpeed1Temp;                     //第一段跳移动参考值
    [SerializeField] private float ySpeed2Temp;                     //多段跳速度参考值
    [SerializeField] public int nTimesJumpTemp;                     //多段跳次数参考值     
    [SerializeField] private float xSpeed,xSpeedTemp;                          //水平速度
    [SerializeField] private LayerMask ground;                      //用来检测地面
    private float ySpeed1, ySpeed2;                                 //多段跳实时速度
    private int nTimesJump;                                         //多段跳次数

    [Header("组件")]
    [SerializeField] private Collider2D playerColl1;                //上碰撞体 
    [SerializeField] private Collider2D ChildrensCol;               //下碰撞体
    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private Animator Anima;

    [Header("受伤相关")]
    [SerializeField] private bool isHurting = false;                //判断是否受伤
    [SerializeField] private Vector2 knockBack_left;                //向左击退
    [SerializeField]private Vector2 knockBack_right;                //向右击退

    [Header("UI相关")]
    [SerializeField] private Text scoreText;
    [SerializeField] private int scoreNum;

    [Header("图层检测相关")]
    [SerializeField] private Vector3 upLeft;                        //用来获取头顶左侧的位置
    [SerializeField]private Vector3 upRight;                        //用来获取头顶右侧的位置
    [SerializeField] private Vector3 midUp;                         //头顶中间的位置
    [SerializeField] private Vector3 down;                          //用来获取下侧位置
    [SerializeField] private Vector3 moveLeft;                      //在头顶卡墙时使角色移动
    /// <summary>
    /// 标记是否在墙体之上
    /// </summary>
    [SerializeField] private bool onLayers=false;

    [Header("Dash")]
    [SerializeField] private float dashTime;
    [SerializeField] private float dashTimeLeft;
    [SerializeField] private float dashSpeed;
    [SerializeField] private bool dashChance;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashDir;
    #endregion
    void Start()
    {
        rbPlayer=GetComponent<Rigidbody2D>();
        playerColl1=GetComponent<BoxCollider2D>();
        Anima=GetComponent<Animator>();
        ChildrensCol = GetComponentInChildren<CapsuleCollider2D>();
    }
    void Update()
    {
        if(!isHurting)Movement();
        SwitchAnimP(ChildrensCol);
        if(!isHurting&&!isDashing)Crouch();
        LayerCheck2();

    }
    private void FixedUpdate()
    {
        if(isDashing)ShadowPool.instance.GetFromPool();
    }
    private void Movement()
    {
        float Hrztmove = Input.GetAxis("Horizontal");
        Face = Input.GetAxisRaw("Horizontal");
        if (Face != 0) dashDir = Face;
        if (Hrztmove != 0&&!isDashing)
        {
            rbPlayer.velocity = new Vector2(Hrztmove * xSpeed, rbPlayer.velocity.y);
            Anima.SetFloat("Running",Mathf.Abs(Hrztmove));
        }
        if(Face!= 0) transform.localScale = new Vector3(Face, 1, 1);
        if (Input.GetButton("Jump")&&!playerColl1.isTrigger)
        {
            if (ySpeed1 > 899.7 && nTimesJump == nTimesJumpTemp)                       //一段跳（长按可以跳的更高）
            {
                rbPlayer.velocity=new Vector2(rbPlayer.velocity.x,ySpeed1/100);
                ySpeed1-=Time.deltaTime;
            }
            if(ySpeed2 > 799.7 && nTimesJump != nTimesJumpTemp&&nTimesJump>0)           //多段跳
            {
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, ySpeed2/100);
                ySpeed2-=Time.deltaTime;
            }
        }
        if (Input.GetButtonUp("Jump") && !playerColl1.isTrigger)
        {
            if (nTimesJump == nTimesJumpTemp)                                           //（一段跳结束）
            {
                nTimesJump = nTimesJumpTemp - 1;
            }
            else if (nTimesJump != nTimesJumpTemp)                                      //多段跳结束
            {
                nTimesJump--;
                ySpeed2 = ySpeed2Temp;
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) Dash();
    }
    /// <summary>
    /// LayerCheck方法用来进行地面检测，以重置多段跳次数以及土狼时间的实现
    /// </summary>
    /// <param name="Col">Player脚下的碰撞体</param>
    public void LayerCheck(Collider2D Col)
    {
        if (onLayers)
        {
            ySpeed1 = ySpeed1Temp;
            ySpeed2 = ySpeed2Temp;
            nTimesJump = nTimesJumpTemp;
            ycJump = ycJumpTemp;//用来延迟检测跳跃
        }
        if (!onLayers)
        {
            
            if (ycJump <= 0&&nTimesJump==nTimesJumpTemp&& !Input.GetButton("Jump"))
            {
                nTimesJump=nTimesJumpTemp - 1;
            }
            else if(ycJump>0)ycJump-=Time.deltaTime;
        }
        Anima.SetBool("Jump", false);
        Anima.SetBool("Fall", false);
    }
    public void SwitchAnimP(Collider2D col)
    {
        if (Input.GetButton("Jump")&&nTimesJump!=0) {
            Anima.SetBool("Jump", true);
            Anima.SetBool("Fall",false);
        }
        if (rbPlayer.velocity.y<0&&!onLayers||rbPlayer.velocity.y<0&&!col.IsTouchingLayers(ground))
        {
            Anima.SetBool("Jump", false);
            Anima.SetBool("Fall", true);
        }
    }
    /// <summary>
    /// ScorePlus方法用来加分/修改角色参数
    /// </summary>
    public void ScorePlus()
    {
        nTimesJumpTemp++;
        scoreNum++;
        scoreText.text=scoreNum.ToString();
    }                   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&&transform.position.y<collision.transform.position.y+0.8&&!isHurting)
        {
            if (transform.position.x <= collision.transform.position.x)
            {
                rbPlayer.velocity = knockBack_left;
            }
            else
            {
                rbPlayer.velocity=knockBack_right;
            }
            isHurting = true;
            Anima.SetBool("Hurting", true);
            Invoke("IsHurtingDelay", 0.5f);
        }
    }
    /// <summary>
    /// IsHurtingDelay方法用来延迟受伤动画
    /// </summary>
    private void IsHurtingDelay()
    {
        isHurting=false;
        Anima.SetBool("Hurting", false);
    }
    private void Crouch()
    {
        if (!(playerColl1.IsTouchingLayers(ground) && playerColl1.isTrigger))
        {
            if (Input.GetButton("Crouch") && onLayers)
            {
                Anima.SetBool("Crouch", true);
                playerColl1.isTrigger = true;
                xSpeed = xSpeedTemp / 2; 
            }
            else
            {
                Anima.SetBool("Crouch", false);
                playerColl1.isTrigger = false;
                xSpeed = xSpeedTemp;
            }
        }
        else if (playerColl1.IsTouchingLayers(ground) && playerColl1.isTrigger)
        {
            if (!onLayers)
            {
                Anima.SetBool("Crouch", false);
                playerColl1.isTrigger = false;
                xSpeed = xSpeedTemp;
                
            }
        }
        
    }
    /// <summary>
    /// LayerCheck2方法用来防止跳跃时头顶会被卡住、以及判断降落时是为下坡还是掉落
    /// </summary>
    private void LayerCheck2()
    {
        if (Physics2D.OverlapCircle(transform.position + upLeft, 0.08f, ground) && !Physics2D.OverlapCircle(transform.position + midUp, 0.2f, ground)&&Input.GetButton("Jump")&&!playerColl1.isTrigger)
        {
            transform.position = transform.position - moveLeft;
        }
        else if (Physics2D.OverlapCircle(transform.position + upRight, 0.08f, ground) && !Physics2D.OverlapCircle(transform.position + midUp, 0.2f, ground)&&Input.GetButton("Jump")&&!playerColl1.isTrigger) 
        {
            transform.position=transform.position+moveLeft;
        }
        if (Physics2D.OverlapCircle(transform.position + down, 0.3f, ground)) onLayers = true;
        else onLayers = false;
    }
    private void Dash()
    {
        if (dashChance)
        {
            isDashing = true;
            rbPlayer.velocity=new Vector2(dashSpeed*dashDir,rbPlayer.velocity.y);
            dashChance = false;
            Invoke("DashDelay", 0.3f);

        }
    }
    private void DashDelay()
    {
        isDashing = false;
    }
    public void GiveDashChance()
    {
        dashChance = true;
    }
}