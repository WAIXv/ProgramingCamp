using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death_Knight : State
{
    private FSM_Knight fsm;
    private Paramater_Knight paramater;
    public Death_Knight(FSM_Knight fsm, Paramater_Knight paramater)
    {
        this.fsm = fsm;
        this.paramater = paramater;
    }
    public override void OnEnter()
    {
        AudioManager.instance.PlayDeathMusic();
        paramater.anim.SetTrigger("isDeath");
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
