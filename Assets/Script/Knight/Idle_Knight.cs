using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Knight : State
{
    private Paramater_Knight paramater;
    private FSM_Knight fsm;
    private float moveDir;
    public Idle_Knight(FSM_Knight fsm, Paramater_Knight paramater)
    {
        this.fsm = fsm;
        this.paramater = paramater;
    }

    public override void OnEnter()
    {
        paramater.anim.SetBool("isIdling", true);
    }

    public override void OnExit()
    {
        paramater.anim.SetBool("isIdling", false);
    }

    public override void OnFixedUpdate()
    {
        moveDir = fsm.Move();
    }

    public override void OnUpdate()
    {
        fsm.AttackCheck();
        fsm.JumpCheck();

        if (moveDir != 0)
        {
            fsm.ChangeState(StateType.Run);
        }

        //����ո��£����л���Jump״̬
        if (paramater.spacePress)
        {
            fsm.ChangeState(StateType.Jump);
            paramater.spacePress = false;
        }

        //����������£����л���Attack״̬
        if (paramater.attackPress)
        {
            fsm.ChangeState(StateType.Attack);
            paramater.attackPress = false;
        }
    }
}
