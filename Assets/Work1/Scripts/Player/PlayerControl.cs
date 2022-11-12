using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator ani { get; private set; }//玩家动画
    public Rigidbody2D rb { get; private set; }//玩家刚体
    public PlayerStateMachine StateMachine { get; private set; }//玩家状态机
    public PlayerInputAction inputAction { get; private set; }//玩家输入事件
    public PlayerData playerData;//玩家数据

    public Player_Idle IdleState { get; private set; }//空闲状态
    public Player_Run MoveState { get; private set; }//移动状态
    public Player_ExtraRun ExtraMoveState { get; private set; }//额外移动状态
    public Player_Jump JumpState { get; private set; }//跳跃状态
    public Player_Fall FallState { get; private set; }//下落状态
    public Player_Land LandState { get; private set; }//落地状态

    public bool onGround = true;//是否位于地面上
    public bool touchWall = false;//是否接触到墙面
    public bool canMove = true;//是否能够移动 
    public int jumpCount = 0;//当前跳跃次数

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new Player_Idle(this, StateMachine, playerData, "idle");
        MoveState = new Player_Run(this, StateMachine, playerData, "run");
        JumpState = new Player_Jump(this, StateMachine, playerData, "jump");
        FallState = new Player_Fall(this, StateMachine, playerData, "fall");
        LandState = new Player_Land(this, StateMachine, playerData, "land");
        ExtraMoveState = new Player_ExtraRun(this, StateMachine, playerData, "extraJump");
    }

    private void Start()
    {
        ani = GetComponent<Animator>();//获取动画组件
        rb = GetComponent<Rigidbody2D>();//获取刚体组件
        inputAction = GetComponent<PlayerInputAction>();
        StateMachine.Initialize(IdleState);//初始化为Idle状态
    }
    public void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        ani.SetBool("ground", isOnGround());//同步地面状态到动画控制台

        onGround = isOnGround();//地面状态
        touchWall = isTouchWall();//墙面接触状态

        canMove = !touchWall;//接触到墙面时无法移动

    }
    public void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicUpdate();
    }



    public void SetVelocityX(float velocityX)//设置x轴速度
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    public void SetVelocityY(float velocityY)//设置y轴速度
    {
        rb.velocity = new Vector2(rb.velocity.x, velocityY);
    }

    public Transform groundCheckPos;//地面检测位置
    public bool isOnGround()//地面检测
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, playerData.groundCheckRadius, playerData.groundCheckLayer);
    }
    public Transform wallCheckPos;//地面检测位置
    public bool isTouchWall()//墙面检测
    {
        return Physics2D.OverlapBox(wallCheckPos.position, playerData.wallCheckRange,0 ,playerData.wallCheckLayer);
    }
}

