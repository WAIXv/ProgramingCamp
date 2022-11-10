using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState stratState)//初始化状态
    {
        CurrentState = stratState;//将当前状态设置为初始状态
        CurrentState.Enter();//进入初始状态
    }

    public void ChangeState(PlayerState nextState)//切换状态
    {
        CurrentState.Exit();//退出当前状态
        CurrentState = nextState;//将当前状态设置为下一状态
        CurrentState.Enter();//进入下一状态
    }
}
