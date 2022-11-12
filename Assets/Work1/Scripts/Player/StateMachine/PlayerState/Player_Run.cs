using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Run : PlayerState
{
    public Player_Run(PlayerControl player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
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
        if (player.inputAction.MoveInput.x == 0&&player.onGround)//位于地面 且 不存在移动输入时 
        {
            stateMachine.ChangeState(player.IdleState);//进入空闲状态
        }

        if (player.isOnGround() == false)//离开地面时
        {
            stateMachine.ChangeState(player.ExtraMoveState);//进入额外移动状态
        }

        if (player.inputAction.JumpInput&&player.jumpCount>0)//剩余跳跃次数 且 跳跃输入存在时 
        {
            stateMachine.ChangeState(player.JumpState);//进入跳跃状态
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
