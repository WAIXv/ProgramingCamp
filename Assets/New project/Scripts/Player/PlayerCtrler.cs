using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrler : MonoBehaviour
{
    #region ��������
    [Header("����ʱ��/�ӳټ�����")]
    [SerializeField] private float ycJumpTemp;                      //�ӳٵ�����ο�ֵ
    private float ycJump;

    [Header("�ƶ�������")]
    [SerializeField] private float Face;
    [SerializeField] private float ySpeed1Temp;                     //��һ�����ƶ��ο�ֵ
    [SerializeField] private float ySpeed2Temp;                     //������ٶȲο�ֵ
    [SerializeField] public int nTimesJumpTemp;                     //����������ο�ֵ     
    [SerializeField] private float xSpeed,xSpeedTemp;                          //ˮƽ�ٶ�
    [SerializeField] private LayerMask ground;                      //����������
    private float ySpeed1, ySpeed2;                                 //�����ʵʱ�ٶ�
    private int nTimesJump;                                         //���������

    [Header("���")]
    [SerializeField] private Collider2D playerColl1;                //����ײ�� 
    [SerializeField] private Collider2D ChildrensCol;               //����ײ��
    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private Animator Anima;

    [Header("�������")]
    [SerializeField] private bool isHurting = false;                //�ж��Ƿ�����
    [SerializeField] private Vector2 knockBack_left;                //�������
    [SerializeField]private Vector2 knockBack_right;                //���һ���

    [Header("UI���")]
    [SerializeField] private Text scoreText;
    [SerializeField] private int scoreNum;

    [Header("ͼ�������")]
    [SerializeField] private Vector3 upLeft;                        //������ȡͷ������λ��
    [SerializeField]private Vector3 upRight;                        //������ȡͷ���Ҳ��λ��
    [SerializeField] private Vector3 midUp;                         //ͷ���м��λ��
    [SerializeField] private Vector3 down;                          //������ȡ�²�λ��
    [SerializeField] private Vector3 moveLeft;                      //��ͷ����ǽʱʹ��ɫ�ƶ�
    /// <summary>
    /// ����Ƿ���ǽ��֮��
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
            if (ySpeed1 > 899.7 && nTimesJump == nTimesJumpTemp)                       //һ�����������������ĸ��ߣ�
            {
                rbPlayer.velocity=new Vector2(rbPlayer.velocity.x,ySpeed1/100);
                ySpeed1-=Time.deltaTime;
            }
            if(ySpeed2 > 799.7 && nTimesJump != nTimesJumpTemp&&nTimesJump>0)           //�����
            {
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, ySpeed2/100);
                ySpeed2-=Time.deltaTime;
            }
        }
        if (Input.GetButtonUp("Jump") && !playerColl1.isTrigger)
        {
            if (nTimesJump == nTimesJumpTemp)                                           //��һ����������
            {
                nTimesJump = nTimesJumpTemp - 1;
            }
            else if (nTimesJump != nTimesJumpTemp)                                      //���������
            {
                nTimesJump--;
                ySpeed2 = ySpeed2Temp;
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) Dash();
    }
    /// <summary>
    /// LayerCheck�����������е����⣬�����ö���������Լ�����ʱ���ʵ��
    /// </summary>
    /// <param name="Col">Player���µ���ײ��</param>
    public void LayerCheck(Collider2D Col)
    {
        if (onLayers)
        {
            ySpeed1 = ySpeed1Temp;
            ySpeed2 = ySpeed2Temp;
            nTimesJump = nTimesJumpTemp;
            ycJump = ycJumpTemp;//�����ӳټ����Ծ
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
    /// ScorePlus���������ӷ�/�޸Ľ�ɫ����
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
    /// IsHurtingDelay���������ӳ����˶���
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
    /// LayerCheck2����������ֹ��Ծʱͷ���ᱻ��ס���Լ��жϽ���ʱ��Ϊ���»��ǵ���
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