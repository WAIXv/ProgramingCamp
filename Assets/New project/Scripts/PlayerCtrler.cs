using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrler : MonoBehaviour
{
    private float ySpeed1,ySpeed2;                                  //�����ʵʱ�ٶ�
    private int nTimesJump,ycJump;                                  //��������ӳٵ�����
    [SerializeField] private float xSpeed;                          //ˮƽ�ٶ�
    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] private float ySpeed1Temp,ySpeed2Temp;         //������ٶȲο�ֵ
    [SerializeField] public int nTimesJumpTemp;                    //����������ο�ֵ         
    [SerializeField] private Collider2D playerColl1;
    [SerializeField] private Collider2D ChildrensCol;
    [SerializeField] private LayerMask ground;
    [SerializeField] private int ycJumpTemp;                        //�ӳٵ�����ο�ֵ
    [SerializeField] private Animator Anima;
    [SerializeField] private bool isHurting=false;                        //�ж��Ƿ�����
    [SerializeField] private Text scoreText;
    [SerializeField] private int scoreNum;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer=GetComponent<Rigidbody2D>();
        playerColl1=GetComponent<BoxCollider2D>();
        Anima=GetComponent<Animator>();
        ChildrensCol = GetComponentInChildren<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHurting)Movement();
        SwitchAnimP(ChildrensCol);
        
    }
    private void FixedUpdate()
    {
        
    }
    private void Movement()
    {
        float Hrztmove = Input.GetAxis("Horizontal");
        float Face = Input.GetAxisRaw("Horizontal");
        if (Hrztmove != 0)
        {
            rbPlayer.velocity = new Vector2(Hrztmove * xSpeed, rbPlayer.velocity.y);
            Anima.SetFloat("Running",Mathf.Abs(Hrztmove));
        }
        if(Face!= 0) transform.localScale = new Vector3(Face, 1, 1);
        if (Input.GetButton("Jump"))
        {
            if (ySpeed1 > 400 && nTimesJump == nTimesJumpTemp)//һ�����������������ĸ��ߣ�
            {
                rbPlayer.velocity=new Vector2(rbPlayer.velocity.x,ySpeed1/100);
                ySpeed1--;
            }
            if(ySpeed2 > 400 && nTimesJump != nTimesJumpTemp&&nTimesJump>0)//�����
            {
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, ySpeed2/100);
                ySpeed2--;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (nTimesJump == nTimesJumpTemp)//��һ����������
            {
                nTimesJump = nTimesJumpTemp - 1;
            }
            else if (nTimesJump != nTimesJumpTemp)//���������
            {
                nTimesJump--;
                ySpeed2 = ySpeed2Temp;
            }
        }
    }
    public void LayerCheck(Collider2D Col)
    {
        if (Col.IsTouchingLayers(ground))
        {
            ySpeed1 = ySpeed1Temp;
            ySpeed2 = ySpeed2Temp;
            nTimesJump = nTimesJumpTemp;
            ycJump = ycJumpTemp;//�����ӳټ����Ծ
        }
        if (!Col.IsTouchingLayers(ground))
        {
            
            if (ycJump == 0&&nTimesJump==nTimesJumpTemp&& !Input.GetButton("Jump"))
            {
                nTimesJump=nTimesJumpTemp - 1;
            }
            else if(ycJump>0)ycJump--;
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
        if (rbPlayer.velocity.y<0&&!col.IsTouchingLayers(ground))
        {
            Anima.SetBool("Jump", false);
            Anima.SetBool("Fall", true);

        }
        Crouch();
    }
    public void ScorePlus()
    {
        nTimesJumpTemp++;
        scoreNum++;
        scoreText.text=scoreNum.ToString();
    }                   //�����ӷֻ��޸Ľ�ɫ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&&transform.position.y<collision.transform.position.y+0.8&&!isHurting)
        {
            if (transform.position.x <= collision.transform.position.x)
            {
                rbPlayer.velocity = new Vector2(-8, 7);
            }
            else
            {
                rbPlayer.velocity=new Vector2(8, 7);
            }
            isHurting = true;
            Anima.SetBool("Hurting", true);
            Invoke("isHurtDelay", 0.5f);
        }
    }
    private void isHurtDelay()
    {
        isHurting=false;
        Anima.SetBool("Hurting", false);
    }
    private void Crouch()
    {
        if(!(playerColl1.IsTouchingLayers(ground)&&playerColl1.isTrigger))
        if (Input.GetButton("Crouch"))
        {
            Anima.SetBool("Crouch", true);
            playerColl1.isTrigger = true;
        }
        else
        {
            Anima.SetBool("Crouch", false);
            playerColl1.isTrigger = false;
        }
    }
}
