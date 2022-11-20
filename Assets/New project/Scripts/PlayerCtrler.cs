using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrler : MonoBehaviour
{
    [SerializeField] private  float xSpeed;
    [SerializeField] private Rigidbody2D rbPlayer;
    private float ySpeed1,ySpeed2;
    private int nTimesJump,ycJump;
    [SerializeField] private float ySpeed1Temp,ySpeed2Temp;
    [SerializeField] private int nTemp;
    [SerializeField] private Collider2D Coll;
    [SerializeField] private LayerMask ground;
    [SerializeField] private int ycJumpTemp;
    [SerializeField] private Animator Anima;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer=GetComponent<Rigidbody2D>();
        Coll=GetComponent<Collider2D>();
        Anima=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnimP();
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
            if (ySpeed1 > 400 && nTimesJump == nTemp)//一段跳（长按可以跳的更高）
            {
                rbPlayer.velocity=new Vector2(rbPlayer.velocity.x,ySpeed1/100);
                ySpeed1--;
            }
            if(ySpeed2 > 400 && nTimesJump != nTemp&&nTimesJump>0)//多段跳
            {
                rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, ySpeed2/100);
                ySpeed2--;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            if (nTimesJump == nTemp)//（一段跳结束）
            {
                nTimesJump = nTemp - 1;
            }
            else if (nTimesJump != nTemp)//多段跳结束
            {
                nTimesJump--;
                ySpeed2 = ySpeed2Temp;
            }
        }
    }
    public void layerCheck(Collider2D Col)
    {
        if (Col.IsTouchingLayers(ground))
        {
            ySpeed1 = ySpeed1Temp;
            ySpeed2 = ySpeed2Temp;
            nTimesJump = nTemp;
            ycJump = ycJumpTemp;//用来延迟检测跳跃
        }
        if (!Col.IsTouchingLayers(ground))
        {
            if (ycJump == 0&&nTimesJump==nTemp&& !Input.GetButton("Jump"))
            {
                nTimesJump=nTemp - 1;
            }
            else if(ycJump>0)ycJump--;
        }
        Anima.SetBool("Jump", false);
        Anima.SetBool("Fall", false);
    }
    public void SwitchAnimP()
    {
        if (Input.GetButtonDown("Jump")) {
            Anima.SetBool("Jump", true);
            Anima.SetBool("Fall",false);
        }
        if (rbPlayer.velocity.y<0)
        {
            Anima.SetBool("Jump", false);
            Anima.SetBool("Fall", true);
        }
    }
    public void ScorePlus()
    {
        nTemp++;
    }
}
