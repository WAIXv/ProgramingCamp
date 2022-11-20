using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Knight : State
{
    private FSM_Knight fsm;
    private Paramater_Knight paramater;
    private float moveDir;
    public Run_Knight(FSM_Knight fSM, Paramater_Knight paramater)
    {
        this.fsm = fSM;
        this.paramater = paramater;
    }
    public override void OnEnter()
    {
        paramater.anim.SetBool("isRunning", true);
    }

    public override void OnExit()
    {
        paramater.anim.SetBool("isRunning", false);
    }

    public override void OnFixedUpdate()
    {
        moveDir = fsm.Move();
    }

    public override void OnUpdate()
    {
        fsm.JumpCheck();
        fsm.AttackCheck();
        
        //如果速度慢下来，就变成Idle状态
        if (Mathf.Abs(moveDir) < 0.05f)
        {
            fsm.ChangeState(StateType.Idle);
        }

        //如果空格按下，就切换到Jump状态
        if (paramater.spacePress)
        {
            fsm.ChangeState(StateType.Jump);
            paramater.spacePress = false;
        }

        //如果攻击按下，就切换到Attack状态
        if (paramater.attackPress)
        {
            fsm.ChangeState(StateType.Attack);
            paramater.attackPress = false;
        }
    }
}
