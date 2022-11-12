using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : PlayerStateMachine
{
    protected PlayerControl player;//玩家控制
    protected PlayerStateMachine stateMachine;//玩家状态机
    protected PlayerData playerData;//玩家数据

    public float stateStartTime;//动画开始时间
    private string animationBoolName;//播放动画名
    public float stateDuration => Time.time - stateStartTime;//动画持续时间

    public PlayerState(PlayerControl player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()//进入状态时
    {
        Checks();
        stateStartTime = Time.time;//记录状态进入时间
        player.ani.SetBool(animationBoolName, true);//将状态名对应bool属性设置为true
    }

    public virtual void Exit()//退出状态时
    {
        player.ani.SetBool(animationBoolName, false);//将状态名对应bool属性设置为true
    }

    public virtual void LogicUpdate()//逻辑更新
    {

    }

    public virtual void PhysicUpdate()//物理更新
    {

    }

    public virtual void Checks()//检测
    {

    }
}
