using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt_Goblin : State
{
    private FSM_Goblin fsm;
    private Paramater_Goblin paramater;
    public Hurt_Goblin(FSM_Goblin fsm, Paramater_Goblin paramater)
    {
        this.fsm = fsm;
        this.paramater = paramater;
    }
    public override void OnEnter()
    {
        paramater.anim.SetTrigger("isHurting");
        paramater.isAttacked = false;
    }

    public override void OnExit()
    {

    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnUpdate()
    {
        
    }
}
