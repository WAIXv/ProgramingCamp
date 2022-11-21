using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run_Goblin : State
{
    private FSM_Goblin fsm;
    private Paramater_Goblin paramater;
    public Run_Goblin(FSM_Goblin fsm, Paramater_Goblin paramater)
    {
        this.fsm = fsm;
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
        //如果目标在左边，就往左边移动;否则往右边移动
        if (paramater.target.transform.position.x < fsm.transform.position.x)
        {
            fsm.transform.localScale = new Vector3(-1, 1, 1);
            paramater.rb.velocity = new Vector2(-1 * paramater.speed, paramater.rb.velocity.y);
        }
        else
        {
            fsm.transform.localScale = new Vector3(1, 1, 1);
            paramater.rb.velocity = new Vector2(paramater.speed, paramater.rb.velocity.y);
        }
    }

    public override void OnUpdate()
    {
        if (!paramater.isPlayerIn)
        {
            fsm.ChangeState(StateType.Idle);
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
