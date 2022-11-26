using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Goblin : MonoBehaviour
{
    private Dictionary<StateType, State> allSaveState;
    [SerializeField]
    private Paramater_Goblin paramater=new Paramater_Goblin();

    private void Awake()
    {
        paramater.anim=GetComponent<Animator>();
        paramater.chaseBound = GetComponent<BoxCollider2D>();
        paramater.rb = GetComponent<Rigidbody2D>();
        paramater.attackHitBox = transform.Find("AttackHitBox").gameObject.GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        allSaveState = new Dictionary<StateType, State>();
        AddState(StateType.Idle, new Idle_Goblin(this, paramater));
        AddState(StateType.Run, new Run_Goblin(this, paramater));
        AddState(StateType.Hurt, new Hurt_Goblin(this, paramater));
        AddState(StateType.Death, new Death_Goblin(this, paramater));
        AddState(StateType.Attack, new Attack_Goblin(this, paramater));
    }

    private void Update()
    {
        allSaveState[paramater.currentState]?.OnUpdate();
        Debug.Log(paramater.currentState);
    }

    private void FixedUpdate()
    {
        if (paramater.currentState != StateType.Hurt) IfAttackCheck();
        allSaveState[paramater.currentState]?.OnFixedUpdate();
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

    //¼ì²â½ÇÉ«ÊÇ·ñ½øÈë×·»÷·¶Î§
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            paramater.isPlayerIn = true;
            paramater.target = collision.gameObject;
        }
    }

    //¼ì²â½ÇÉ«ÊÇ·ñÀë¿ª×·»÷·¶Î§
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            paramater.isPlayerIn = false;
            paramater.target = null;
        }
    }

    //Ôâµ½¹¥»÷
    public void Attacked()
    {
        paramater.isAttacked = true;
    }

    //¿ÛÑª
    public void DecreaseHP(int n)
    {
        paramater.hp -= n;
        if (paramater.hp <= 0)
        {
            ChangeState(StateType.Death);
        }
    }

    //¶¯»­Ö¡ÊÂ¼þ£¬ÊÜÉË½áÊø
    public void EndHurt()
    {
        ChangeState(StateType.Idle);
    }

    //¶¯»­Ö¡ÊÂ¼þ£¬µÐÈËËÀÍö
    public void Death()
    {
        Destroy(this.gameObject);
    }

    //ÅÐ¶ÏÍæ¼ÒÊÇ·ñÔÚ¹¥»÷·¶Î§ÄÚ
    public void IfAttackCheck()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(paramater.hand.transform.position, transform.right * transform.localScale.x, 0.5f,paramater.playerLayer);
        if (hit && hit.collider.gameObject.CompareTag("Player"))
        {
            paramater.canAttack = true;
        }
        else paramater.canAttack = false;
    }

    //¶¯»­Ö¡ÊÂ¼þ£¬ÍË³ö¹¥»÷
    public void ExitAttack()
    {
        ChangeState(StateType.Idle);
    }

    //½øÈë¹¥»÷
    public void EnterAttack()
    {
        StartCoroutine(StartAttack(paramater.attackStartTime, paramater.attackHoldTime));
    }

    public IEnumerator StartAttack(float attackStartTime, float attackHoldTime)
    {
        yield return new WaitForSeconds(attackStartTime);
        paramater.attackHitBox.enabled = true;
        yield return new WaitForSeconds(attackHoldTime);
        paramater.attackHitBox.enabled = false;
    }
}
