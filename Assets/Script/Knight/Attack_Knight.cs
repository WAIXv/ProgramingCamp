using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Knight : State
{
    private FSM_Knight fsm;
    private Paramater_Knight paramater;
    public Attack_Knight(FSM_Knight fsm, Paramater_Knight paramater)
    {
        this.fsm = fsm;
        this.paramater = paramater;
    }

    public override void OnEnter()
    {
        paramater.anim.SetTrigger("isAttacking");
        fsm.EnterAttack();
    }

    public override void OnExit()
    {
        
    }

    public override void OnFixedUpdate()
    {
        fsm.Move();
    }

    public override void OnUpdate()
    {
        fsm.JumpCheck();
        if (paramater.spacePress)
        {
            fsm.ChangeState(StateType.Jump);
            paramater.spacePress = false;
        }
    }
}
