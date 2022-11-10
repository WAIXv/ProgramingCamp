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
    public Player_Jump JumpState { get; private set; }//跳跃状态
    public Player_Fall FallState { get; private set; }//下落状态
    public Player_Land LandState { get; private set; }//落地状态

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new Player_Idle(this, StateMachine, playerData, "idle");
        MoveState = new Player_Run(this, StateMachine, playerData, "run");
        JumpState = new Player_Jump(this, StateMachine, playerData, "jump");
        FallState = new Player_Fall(this, StateMachine, playerData, "fall");
        LandState = new Player_Land(this, StateMachine, playerData, "land");
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
        ani.SetBool("ground", isOnGround());
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
    public bool isOnGround()//进行地面检测
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, playerData.groundCheckRadius, playerData.groundCheckLayer);
    }

    public Transform wallCheckPos;//地面检测位置
    public bool isTouchWall()//进行墙面检测
    {
        return Physics2D.OverlapBox(wallCheckPos.position, playerData.wallCheckRange,0 ,playerData.wallCheckLayer);
    }
}

