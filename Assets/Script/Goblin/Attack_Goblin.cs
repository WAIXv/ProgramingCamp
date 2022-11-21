using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Goblin : State
{
    private FSM_Goblin fsm;
    private Paramater_Goblin paramater;
    public Attack_Goblin(FSM_Goblin fsm, Paramater_Goblin paramater)
    {
        this.fsm = fsm;
        this.paramater = paramater;
    }
    public override void OnEnter()
    {
        paramater.anim.SetBool("isAttacking", true);
        fsm.EnterAttack();
    }

    public override void OnExit()
    {
        paramater.anim.SetBool("isAttacking", false);
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}
