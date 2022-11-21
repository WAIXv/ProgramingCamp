using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Goblin : State
{
    private FSM_Goblin fsm;
    private Paramater_Goblin paramater;
    public Death_Goblin(FSM_Goblin fsm, Paramater_Goblin paramater)
    {
        this.fsm = fsm;
        this.paramater = paramater;
    }
    public override void OnEnter()
    {
        paramater.anim.SetBool("isDeath", true);
    }

    public override void OnExit()
    {
        paramater.anim.SetBool("isDeath", false);
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnUpdate()
    {

    }
}
