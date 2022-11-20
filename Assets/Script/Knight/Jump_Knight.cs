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
        //进入Jump状态时跳跃
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

        //如果竖直速度小于0，说明正在下落，切换到Fall状态
        if (paramater.rb.velocity.y < -0.05f) 
            fsm.ChangeState(StateType.Fall);

        //如果攻击按下，就切换到Attack状态
        if (paramater.attackPress)
        {
            fsm.ChangeState(StateType.Attack);
            paramater.attackPress = false;
        }
    }
}
