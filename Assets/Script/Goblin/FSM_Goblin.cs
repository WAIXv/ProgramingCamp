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
    }

    private void Start()
    {
        allSaveState = new Dictionary<StateType, State>();
        AddState(StateType.Idle, new Idle_Goblin(this, paramater));
        AddState(StateType.Run, new Run_Goblin(this, paramater));
    }

    private void Update()
    {
        allSaveState[paramater.currentState]?.OnUpdate();
    }

    private void FixedUpdate()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            paramater.isPlayerIn = true;
            paramater.target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            paramater.isPlayerIn = false;
            paramater.target = null;
        }
    }

    //¿ÛÑª
    public void DecreaseHP(int n)
    {
        paramater.hp -= n;
        if (paramater.hp <= 0) Destroy(this.gameObject);
    }
}
