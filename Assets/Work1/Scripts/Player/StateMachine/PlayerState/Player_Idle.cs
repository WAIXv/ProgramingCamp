using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Idle : PlayerState
{
    public Player_Idle(PlayerControl player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {

    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0);//将角色速度归零
        player.jumpCount = playerData.maxJumpCount;//重置剩余跳跃次数为最大跳跃次数
    }

    public override void Exit()
    {
        base.Exit();
        player.transform.localScale = new Vector2(((player.inputAction.MoveInput.x > 0) ? 1 : -1), 1);//设置翻转方向
    }

    public override void LogicUpdate()//可切入状态:移动 跳跃 下落
    {
        base.LogicUpdate();

        if(player.isOnGround()==false)//不在地面时 进入下落状态
        {
            stateMachine.ChangeState(player.FallState);
        }

        if (player.inputAction.MoveInput.x != 0)//移动输入存在时 进入移动状态
        {
            stateMachine.ChangeState(player.MoveState);
        }

        if (player.inputAction.JumpInput)//跳跃输入存在时 进入跳跃状态
        {
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
