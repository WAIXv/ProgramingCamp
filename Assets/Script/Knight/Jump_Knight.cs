using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Knight : State
{
    private FSM_Knight fsm;
    public Jump_Knight(FSM_Knight fsm)
    {
        this.fsm = fsm;
    }

    public override void OnEnter()
    {
        fsm.anim.SetBool("isJumping", true);
        //进入Jump状态时跳跃
        fsm.rb.AddForce(new Vector2(0, fsm.jumpForce));
    }

    public override void OnExit()
    {
        fsm.anim.SetBool("isJumping", false);
    }

    public override void OnUpdate()
    {
        fsm.Move();

        //如果竖直速度小于0，说明正在下落，切换到Fall状态
        if (fsm.rb.velocity.y < 0) 
            fsm.ChangeState(StateType.Fall);
    }
}
