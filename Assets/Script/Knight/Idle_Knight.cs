using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Knight : State
{
    private FSM_Knight fsm;
    public Idle_Knight(FSM_Knight fSM)
    {
        this.fsm = fSM;
    }

    public override void OnEnter()
    {
        fsm.anim.SetBool("isIdling", true);
        //清空跳跃时按下的空格
        fsm.spacePress = false;
    }

    public override void OnExit()
    {
        fsm.anim.SetBool("isIdling", false);
    }

    public override void OnUpdate()
    {
        //如果角色正在水平移动，就切换到Run状态
        if (fsm.Move() != 0) 
        {
            fsm.ChangeState(StateType.Run);
        }

        //如果空格按下，就切换到Jump状态
        if (fsm.spacePress)
        {
            fsm.ChangeState(StateType.Jump);
            fsm.spacePress = false;
        }
    }
}
