using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Goblin : State
{
    private FSM_Goblin fsm;
    private Paramater_Goblin paramater;
    public Idle_Goblin(FSM_Goblin fsm, Paramater_Goblin paramater)
    {
        this.fsm = fsm;
        this.paramater = paramater;
    }

    public override void OnEnter()
    {
        paramater.rb.velocity = Vector2.zero;
        paramater.anim.SetBool("isIdling", true);
    }

    public override void OnExit()
    {
        paramater.anim.SetBool("isIdling", false);
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnUpdate()
    {
        if (paramater.isPlayerIn)
        {
            fsm.ChangeState(StateType.Run);
        }

        if (paramater.isAttacked)
        {
            fsm.ChangeState(StateType.Hurt);
        }

        if (paramater.canAttack)
        {
            fsm.ChangeState(StateType.Attack);
        }
    }
}
