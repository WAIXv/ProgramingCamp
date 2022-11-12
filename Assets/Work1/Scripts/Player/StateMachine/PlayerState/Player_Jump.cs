using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : PlayerState
{
    public Player_Jump(PlayerControl player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {

    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(playerData.jumpForce);//读取跳跃力度数据 进行跳跃
        player.jumpCount--;//跳跃次数-1
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocityY(0);//将刚体y轴速度归零
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.rb.velocity.y < 0 && !player.isOnGround()) //若刚体y轴速度小于0 且不位于地面
            stateMachine.ChangeState(player.FallState);//则进入下落状态

        if(!player.inputAction.JumpInput) //若停止输入跳跃
            stateMachine.ChangeState(player.FallState);//则进入下落状态

        player.ani.SetFloat("speedY", Mathf.Abs(0.4f));//传递同步动画状态
    }

    public override void PhysicUpdate()
    {

    }
}