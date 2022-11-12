using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ExtraRun : PlayerState
{
    public Player_ExtraRun(PlayerControl player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {

    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        base.Enter();
        player.rb.gravityScale = 0;//将重力设置为0
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if (player.jumpCount > 0 && player.inputAction.JumpInput)//剩余跳跃次数 且 按下跳跃键时
        {
            stateMachine.ChangeState(player.JumpState);//进入跳跃状态
            player.jumpCount--;//跳跃次数-1
            player.rb.gravityScale = playerData.gravityMount;//重置重力参数
        }

        if(player.inputAction.MoveInput.x == 0||stateDuration>playerData.extraTime)
            //玩家停止移动输入 或者时间超过额外时间时
        {
            stateMachine.ChangeState(player.FallState);
            player.rb.gravityScale = playerData.gravityMount;//重置重力参数
        }
    }

    public override void PhysicUpdate()
    {
        if (!player.isTouchWall()) player.SetVelocityX(playerData.moveSpeed * player.inputAction.MoveInput.x);//设置水平移动
    }
}

