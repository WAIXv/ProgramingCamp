using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Knight : State
{
    private FSM_Knight fsm;
    private Paramater_Knight paramater;
    public Fall_Knight(FSM_Knight fsm, Paramater_Knight paramater)
    {
        this.fsm = fsm;
        this.paramater = paramater;
    }

    public override void OnEnter()
    {
        paramater.anim.SetBool("isFalling",true);
    }

    public override void OnExit()
    {
        paramater.anim.SetBool("isFalling", false);
    }

    public override void OnFixedUpdate()
    {
        fsm.Move();
    }

    public override void OnUpdate()
    {
        fsm.AttackCheck();
        //如果触碰到地面，就切换为Idle状态
        if (paramater.isGround)
        {
            fsm.ChangeState(StateType.Idle);
        }

        //如果攻击按下，就切换到Attack状态
        if (paramater.attackPress)
        {
            fsm.ChangeState(StateType.Attack);
            paramater.attackPress = false;
        }
    }
}
