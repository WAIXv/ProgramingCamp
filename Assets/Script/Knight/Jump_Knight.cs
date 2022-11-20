using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Knight : State
{
    private FSM_Knight fsm;
    private Paramater_Knight paramater;
    public Jump_Knight(FSM_Knight fsm, Paramater_Knight paramater)
    {
        this.fsm = fsm;
        this.paramater = paramater;
    }

    public override void OnEnter()
    {
        paramater.anim.SetBool("isJumping", true);
        //����Jump״̬ʱ��Ծ
        if (paramater.isGround)
            paramater.rb.AddForce(new Vector2(0, paramater.jumpForce));
    }

    public override void OnExit()
    {
        paramater.anim.SetBool("isJumping", false);
    }

    public override void OnFixedUpdate()
    {
        fsm.Move();
    }

    public override void OnUpdate()
    {
        fsm.AttackCheck();

        //�����ֱ�ٶ�С��0��˵���������䣬�л���Fall״̬
        if (paramater.rb.velocity.y < -0.05f) 
            fsm.ChangeState(StateType.Fall);

        //����������£����л���Attack״̬
        if (paramater.attackPress)
        {
            fsm.ChangeState(StateType.Attack);
            paramater.attackPress = false;
        }
    }
}
