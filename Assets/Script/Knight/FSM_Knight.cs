using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,Run,Jump,Fall,Attack,Death,Hurt
}

public class FSM_Knight : MonoBehaviour
{
    private Dictionary<StateType, State> allSaveState;
    [SerializeField]
    private Paramater_Knight paramater = new Paramater_Knight();
    private void Awake()
    {
        allSaveState = new Dictionary<StateType, State>();
        AddState(StateType.Idle, new Idle_Knight(this, paramater));
        AddState(StateType.Run, new Run_Knight(this, paramater));
        AddState(StateType.Jump, new Jump_Knight(this, paramater));
        AddState(StateType.Fall, new Fall_Knight(this, paramater));
        AddState(StateType.Attack, new Attack_Knight(this, paramater));
    }
    void Start()
    {
        paramater.trans = this.transform;
        paramater.anim = GetComponent<Animator>();
        paramater.rb = GetComponent<Rigidbody2D>();
        paramater.coll = GetComponent<Collider2D>();
        paramater.attackHitBox = transform.Find("AttackHitBox").gameObject.GetComponent<PolygonCollider2D>();
        paramater.allSaveState = this.allSaveState;
        ChangeState(StateType.Idle);
    }

    private void Update()
    {
        allSaveState[paramater.currentState]?.OnUpdate();
    }
    void FixedUpdate()
    {
        PhysicsCheck();
        allSaveState[paramater.currentState]?.OnFixedUpdate();
        Debug.Log(paramater.currentState);
    }

    //Ìí¼Ó×´Ì¬
    private void AddState(StateType type, State state)
    {
        if (allSaveState.ContainsKey(type)) return;
        allSaveState.Add(type, state);
    }

    //ÇÐ»»×´Ì¬
    public void ChangeState(StateType nextState)
    {
        if (allSaveState[paramater.currentState] == allSaveState[nextState]) return;
        allSaveState[paramater.currentState]?.OnExit();
        paramater.lastState = paramater.currentState;
        paramater.currentState = nextState;
        allSaveState[paramater.currentState]?.OnEnter();
    }

    //°´¼ü¼ì²â
    public void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            paramater.spacePress = true;
        }

    }

    public void AttackCheck()
    {
        if (Input.GetKeyDown(KeyCode.J))
            paramater.attackPress = true;
    }

    //ÎïÀí¼ì²â
    public void PhysicsCheck()
    {
        RaycastHit2D leftFootHit;
        RaycastHit2D rightFootHit;
        RaycastHit2D grabHandHit;
        leftFootHit = Physics2D.Raycast(paramater.leftFoot.transform.position, Vector3.down, 0.1f, paramater.groundLayer);
        rightFootHit = Physics2D.Raycast(paramater.rightFoot.transform.position, Vector3.down, 0.1f, paramater.groundLayer);
        grabHandHit = Physics2D.Raycast(paramater.grabHand.transform.position, transform.right * transform.localScale.x, 0.1f, paramater.groundLayer);
        paramater.isGround = leftFootHit || rightFootHit;
        paramater.isOnWall = grabHandHit;
        //Debug.DrawRay(paramater.leftFoot.transform.position, Vector3.down * 0.1f, Color.red);
        //Debug.DrawRay(paramater.rightFoot.transform.position, Vector3.down * 0.1f, Color.red);
        //Debug.DrawRay(paramater.grabHand.transform.position, transform.right * transform.localScale.x * 0.1f, Color.red);
    }

    //½ÇÉ«ÒÆ¶¯
    public float Move()
    {
        float moveDir = Input.GetAxisRaw("Horizontal");
        if (moveDir != 0) transform.localScale = new Vector3(moveDir, 1, 1);
        paramater.rb.velocity = new Vector2(paramater.speed * moveDir, paramater.rb.velocity.y);
        return moveDir;
    }

    //ÊÕ¼¯
    private void Collect()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collection")
        {
            Destroy(collision.gameObject);
        }
    }

    //½øÈë¹¥»÷
    public void EnterAttack()
    {
        StartCoroutine(StartAttack(paramater.attackStartTime, paramater.attackHoldTime));
    }
    //ÍË³ö¹¥»÷
    public void ExitAttack()
    {
        if (paramater.lastState == StateType.Jump) ChangeState(StateType.Fall);
        else ChangeState(paramater.lastState);
    }

    public IEnumerator StartAttack(float attackStartTime,float attackHoldTime)
    {
        yield return new WaitForSeconds(attackStartTime);
        paramater.attackHitBox.enabled = true;
        yield return new WaitForSeconds(attackHoldTime);
        paramater.attackHitBox.enabled = false;
    }

    //¿ÛÑª
    public void DecreaseHP(int n)
    {
        paramater.hp -= n;
    }
}
