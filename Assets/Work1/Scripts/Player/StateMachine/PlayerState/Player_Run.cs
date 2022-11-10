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
        if (player.inputAction.MoveInput.x == 0)//不存在移动输入时 进入空闲状态
        {
            stateMachine.ChangeState(player.IdleState);
        }
        if (player.inputAction.JumpInput)//跳跃输入存在时 进入跳跃状态
        {
            stateMachine.ChangeState(player.JumpState);
        }

    }

    public override void PhysicUpdate()
    {
       if(!player.isTouchWall()) player.SetVelocityX(playerData.moveSpeed * player.inputAction.MoveInput.x);//设置水平移动
    }
}
