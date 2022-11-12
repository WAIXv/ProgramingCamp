using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Fall : PlayerState
{
    public Player_Fall(PlayerControl player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {

    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(player.inputAction.JumpInput&&player.jumpCount>0)//剩余跳跃次数 且 按下跳跃键时
        {
            stateMachine.ChangeState(player.JumpState);//切入跳跃状态
        }

        if (player.isOnGround() && player.rb.velocity.y < 0.01f)//位于地面且刚体y轴速度小于0.01时
        {
            stateMachine.ChangeState(player.LandState);//切换至落地状态
        }

    }

    public override void PhysicUpdate()
    {
        if (player.canMove)//能够移动时 
        {
            player.SetVelocityX(playerData.moveSpeed * player.inputAction.MoveInput.x);//进行空中左右移动
            player.transform.localScale = new Vector2(((player.inputAction.MoveInput.x > 0) ? 1 : -1), 1);//设置翻转方向
            player.ani.SetFloat("speedY", Mathf.Abs(0.6f));//传递同步动画状态
        }
    }
}
