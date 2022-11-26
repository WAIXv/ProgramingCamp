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

    //���״̬
    private void AddState(StateType type, State state)
    {
        if (allSaveState.ContainsKey(type)) return;
        allSaveState.Add(type, state);
    }

    //�л�״̬
    public void ChangeState(StateType nextState)
    {
        if (allSaveState[paramater.currentState] == allSaveState[nextState]) return;
        allSaveState[paramater.currentState]?.OnExit();
        paramater.lastState = paramater.currentState;
        paramater.currentState = nextState;
        allSaveState[paramater.currentState]?.OnEnter();
    }

    //����ɫ�Ƿ����׷����Χ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            paramater.isPlayerIn = true;
            paramater.target = collision.gameObject;
        }
    }

    //����ɫ�Ƿ��뿪׷����Χ
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            paramater.isPlayerIn = false;
            paramater.target = null;
        }
    }

    //�⵽����
    public void Attacked()
    {
        paramater.isAttacked = true;
    }

    //��Ѫ
    public void DecreaseHP(int n)
    {
        paramater.hp -= n;
        if (paramater.hp <= 0)
        {
            ChangeState(StateType.Death);
        }
    }

    //����֡�¼������˽���
    public void EndHurt()
    {
        ChangeState(StateType.Idle);
    }

    //����֡�¼�����������
    public void Death()
    {
        Destroy(this.gameObject);
    }

    //�ж�����Ƿ��ڹ�����Χ��
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

    //����֡�¼����˳�����
    public void ExitAttack()
    {
        ChangeState(StateType.Idle);
    }

    //���빥��
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
