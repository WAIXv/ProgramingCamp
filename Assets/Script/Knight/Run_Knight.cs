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
        
        //����ٶ����������ͱ��Idle״̬
        if (Mathf.Abs(moveDir) < 0.05f)
        {
            fsm.ChangeState(StateType.Idle);
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
