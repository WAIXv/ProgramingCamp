using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Knight : State
{
    private FSM_Knight fsm;
    public Run_Knight(FSM_Knight fSM)
    {
        this.fsm = fSM;
    }
    public override void OnEnter()
    {
        fsm.anim.SetBool("isRunning", true);
    }

    public override void OnExit()
    {
        fsm.anim.SetBool("isRunning", false);
    }

    public override void OnUpdate()
    {
        float move = fsm.Move();

        //如果速度慢下来，就变成Idle状态
        if (Mathf.Abs(move) < 0.05f)
        {
            fsm.ChangeState(StateType.Idle);
        }

        //如果空格按下，就切换到Jump状态
        if (fsm.spacePress)
        {
            fsm.ChangeState(StateType.Jump);
            fsm.spacePress = false;
        }
    }
}
