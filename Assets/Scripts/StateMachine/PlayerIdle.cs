using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using static PlayerFSM;

public class PlayerIdle : IState
{
    PlayerFSM manager;
    Parameter Parameter;
    public PlayerIdle(PlayerFSM manager)
    {
        this.manager = manager;
        Parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Parameter.animator.Play("HeroKnight_Idle");
    }
    public void OnUpdate()
    {
        if (Parameter.FaceDir != 0)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.Run);
        }
        if (Parameter.IsJump && Parameter.IsOnGround)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.Jump);
        }
        if (Parameter.PlayerRigidbody.velocity.y < 0 && !Parameter.IsOnGround)
        {
            manager.transitionState(PlayerStateType.Fall);
        }
        if (Parameter.FaceDir == 0 && Mathf.Abs(Parameter.PlayerRigidbody.velocity.x) > 1)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.SlowDown);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            manager.transitionState(PlayerStateType.Attack1);
        }
        if(Parameter.IsRoll)
        {
            manager.transitionState(PlayerStateType.Roll);
        }
    }
    public void OnFixedUpdate()
    {

    }

    public void OnExit()
    {

    }
}
public class PlayerRun : IState
{
    PlayerFSM manager;
    Parameter Parameter;
    public PlayerRun(PlayerFSM manager)
    {
        this.manager = manager;
        Parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Parameter.animator.Play("HeroKnight_Run");

    }
    public void OnUpdate()
    {
        

        if (Parameter.FaceDir == 0)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.SlowDown);
        }
        if(Parameter.IsJump && (Parameter.IsOnGround || Parameter.toyoTimer > 0))
        {
            manager.transitionState(PlayerFSM.PlayerStateType.Jump);
        }
        if (Parameter.PlayerRigidbody.velocity.y < 0 && (!Parameter.IsOnGround || Parameter.toyoTimer <= 0))
        {
            manager.transitionState(PlayerStateType.Fall);
        }
        if (Parameter.IsRoll)
        {
            manager.transitionState(PlayerStateType.Roll);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            manager.transitionState(PlayerStateType.Attack1);
        }
        if (Parameter.FaceDir != 0) manager.transform.localScale= new Vector3(Parameter.FaceDir,1,1);
    }
    public void OnFixedUpdate()
    {
        if (Parameter.FaceDir != 0 && Parameter.RunPercent < 1)
        {
            Parameter.RunPercent += Time.fixedDeltaTime / Parameter.RunTime;
        }
        Parameter.PlayerRigidbody.velocity = new Vector2(Parameter.MaxMoveSpeed * Parameter.RunSpeedCurve.Evaluate(Parameter.RunPercent) * Parameter.PlayerTransform.localScale.x, Parameter.PlayerRigidbody.velocity.y);
    }
    public void OnExit()
    {

    }
}
public class PlayerSlowDown : IState
{
    PlayerFSM manager;
    Parameter Parameter;
    public PlayerSlowDown(PlayerFSM manager)
    {
        this.manager = manager;
        Parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Parameter.SpeedXInEndRun = Parameter.PlayerRigidbody.velocity.x;
        Parameter.animator.Play("HeroKnight_SlowDown");
    }
    public void OnUpdate()
    {
        if (Parameter.FaceDir != 0)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.Run);
        }
        if (Parameter.IsJump && Parameter.IsOnGround)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.Jump);
        }
        if (Mathf.Abs(Parameter.PlayerRigidbody.velocity.x) <= 0.1f)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.Idle);
        }
        if(Parameter.IsRoll)
        {
            manager.transitionState(PlayerStateType.Roll);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            manager.transitionState(PlayerStateType.Attack1);
        }
        if (Parameter.FaceDir != 0) manager.transform.localScale = new Vector3(Parameter.FaceDir, 1, 1);
    }
    public void OnFixedUpdate()
    {
        Parameter.RunPercent -= Time.fixedDeltaTime;
        Parameter.PlayerRigidbody.velocity = new Vector2(Parameter.SpeedXInEndRun * Parameter.SlowDownSpeedCurve.Evaluate(Parameter.RunPercent),Parameter.PlayerRigidbody.velocity.y);
    }
    public void OnExit()
    {
        Parameter.RunPercent = 0;
    }
}
public class PlayerRoll : IState
{
    PlayerFSM manager;
    Parameter Parameter;
    public PlayerRoll(PlayerFSM manager)
    {
        this.manager = manager;
        Parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Parameter.SpeedXInEndRun = Parameter.PlayerRigidbody.velocity.x;
        Parameter.animator.Play("HeroKnight_Roll");
        Parameter.PlayerRigidbody.drag = 1;
        Parameter.PlayerRigidbody.AddForce(new Vector2(Mathf.Abs(Parameter.SpeedXInEndRun) > 4 ? Parameter.SpeedXInEndRun * 1.5f:8 * Parameter.PlayerTransform.localScale.x, 0), ForceMode2D.Impulse);
    }
    public void OnUpdate()
    {
        if(Parameter.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if(Parameter.PlayerRigidbody.velocity.y > 0)
            {
                manager.transitionState(PlayerFSM.PlayerStateType.SlowDown);
            }
            if(Parameter.PlayerRigidbody.velocity.y < 0)
            {
                manager.transitionState(PlayerFSM.PlayerStateType.Fall);
            }
        }

        if (Parameter.FaceDir != 0) manager.transform.localScale = new Vector3(Parameter.FaceDir, 1, 1);
    }
    public void OnFixedUpdate()
    {
        //Parameter.RunPercent -= Time.fixedDeltaTime * 0.1f;
        //Parameter.PlayerRigidbody.velocity = new Vector2(Parameter.SpeedXInEndRun * Parameter.SlowDownSpeedCurve.Evaluate(Parameter.RunPercent), Parameter.PlayerRigidbody.velocity.y);
    }
    public void OnExit()
    {
        Parameter.PlayerRigidbody.drag = 0f;
    }
}
public class PlayerJump : IState
{
    PlayerFSM manager;
    Parameter Parameter;
    public PlayerJump(PlayerFSM manager)
    {
        this.manager = manager;
        Parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Parameter.animator.Play("HeroKnight_Jump");
    }
    public void OnUpdate()
    {
        if(!Parameter.IsJump||Parameter.JumpPercent >=1)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.Fall);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            manager.transitionState(PlayerStateType.Attack1);
        }
    }
    public void OnFixedUpdate()
    {
        if (Parameter.IsJump && Parameter.JumpPercent < 1)
        {
            Parameter.JumpPercent += Time.fixedDeltaTime / Parameter.JumpTime;
        }
        Parameter.PlayerRigidbody.velocity += new Vector2(0, Parameter.MaxJumpSpeed * Parameter.JumpSpeedCurve.Evaluate(Parameter.JumpPercent) * Time.fixedDeltaTime);
        if (Parameter.FaceDir != 0 && Parameter.RunPercent < 1)
        {
            Parameter.RunPercent += Time.fixedDeltaTime / Parameter.RunTime;
        }
        if (Parameter.FaceDir == 0 && Parameter.RunPercent < 1 && Parameter.RunPercent > 0)
        {
            Parameter.RunPercent -= (Time.fixedDeltaTime / Parameter.RunTime) * Parameter.RunPercent;
        }
        Parameter.PlayerRigidbody.velocity = new Vector2(Parameter.MaxMoveSpeed * Parameter.RunSpeedCurve.Evaluate(Parameter.RunPercent) * Parameter.PlayerTransform.localScale.x, Parameter.PlayerRigidbody.velocity.y);
        if (Parameter.FaceDir != 0) manager.transform.localScale = new Vector3(Parameter.FaceDir, 1, 1);
    }
    public void OnExit()
    {
        Parameter.JumpPercent = 0;
    }
}

public class PlayerFall : IState
{
    PlayerFSM manager;
    Parameter Parameter;
    public PlayerFall(PlayerFSM manager)
    {
        this.manager = manager;
        Parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Parameter.animator.Play("HeroKnight_Fall");
        Parameter.SpeedXInEndRun = (Mathf.Abs(Parameter.PlayerRigidbody.velocity.x)>0 ? Parameter.PlayerRigidbody.velocity.x : Parameter.MaxMoveSpeed);
    }
    public void OnUpdate()
    {
        if(Parameter.IsOnGround && Mathf.Abs(Parameter.PlayerRigidbody.velocity.x) > 1 && Parameter.FaceDir == 0)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.SlowDown);
        }
        if (Parameter.IsOnGround && Mathf.Abs(Parameter.PlayerRigidbody.velocity.x) > 1 && Parameter.FaceDir != 0)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.Run);
        }
        if (Parameter.IsOnGround && Mathf.Abs(Parameter.PlayerRigidbody.velocity.x) < 1)
        {
            manager.transitionState(PlayerFSM.PlayerStateType.Idle);
        }
        if (Parameter.IsJump && (Parameter.IsOnGround || Parameter.toyoTimer > 0))
        {
            manager.transitionState(PlayerFSM.PlayerStateType.Jump);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            manager.transitionState(PlayerStateType.Attack1);
        }
    }
    public void OnFixedUpdate()
    {
        if (Parameter.FaceDir != 0 && Parameter.RunPercent < 1)
        {
            Parameter.RunPercent += Time.fixedDeltaTime / Parameter.RunTime;
        }
        if (Parameter.FaceDir == 0 && Parameter.RunPercent < 1 && Parameter.RunPercent > 0)
        {
            Parameter.RunPercent -= (Time.fixedDeltaTime / Parameter.RunTime) * Parameter.RunPercent;
        }
        Parameter.PlayerRigidbody.velocity = new Vector2(Mathf.Abs(Parameter.SpeedXInEndRun) * Parameter.RunSpeedCurve.Evaluate(Parameter.RunPercent) * Parameter.PlayerTransform.localScale.x, Parameter.PlayerRigidbody.velocity.y);
        if (Parameter.FaceDir != 0) manager.transform.localScale = new Vector3(Parameter.FaceDir, 1, 1);
    }
    public void OnExit()
    {
        Parameter.PlayerRigidbody.mass = 1;
    }
}
public class PlayerAttack1 : IState
{
    PlayerFSM manager;
    Parameter Parameter;
    public PlayerAttack1(PlayerFSM manager)
    {
        this.manager = manager;
        Parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Parameter.PlayerRigidbody.velocity = Vector2.zero;
        Parameter.animator.Play("HeroKnight_Attack1");
    }
    public void OnUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Parameter.AttackNext = true;
        }
        if(Parameter.animator.GetCurrentAnimatorStateInfo(0).normalizedTime>1)
        {
            if (!Parameter.AttackNext)
            {
                manager.transitionState(PlayerStateType.Idle);
            }
            else
            {
                manager.transitionState(PlayerStateType.Attack2);
            }
              
        }
    }
    public void OnFixedUpdate()
    {
        
    }
    public void OnExit()
    {
        Parameter.AttackNext = false;
    }
}

public class PlayerAttack2 : IState
{
    PlayerFSM manager;
    Parameter Parameter;
    public PlayerAttack2(PlayerFSM manager)
    {
        this.manager = manager;
        Parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Parameter.PlayerRigidbody.velocity = Vector2.zero;
        Parameter.animator.Play("HeroKnight_Attack2");
    }
    public void OnUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Parameter.AttackNext = true;
        }
        if (Parameter.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (!Parameter.AttackNext)
            {
                manager.transitionState(PlayerStateType.Idle);
            }
            else
            {
                manager.transitionState(PlayerStateType.Attack3);
            }

        }
    }
    public void OnFixedUpdate()
    {

    }
    public void OnExit()
    {
        Parameter.AttackNext = false;
    }
}
public class PlayerAttack3 : IState
{
    PlayerFSM manager;
    Parameter Parameter;
    public PlayerAttack3(PlayerFSM manager)
    {
        this.manager = manager;
        Parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Parameter.PlayerRigidbody.velocity = Vector2.zero;
        Parameter.animator.Play("HeroKnight_Attack3");
    }
    public void OnUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Parameter.AttackNext = true;
        }
        if (Parameter.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (!Parameter.AttackNext)
            {
                manager.transitionState(PlayerStateType.Idle);
            }
            else
            {
                manager.transitionState(PlayerStateType.Attack1);
            }

        }
    }
    public void OnFixedUpdate()
    {

    }
    public void OnExit()
    {
        Parameter.AttackNext = false;
    }
}
