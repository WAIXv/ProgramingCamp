using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,Run,Jump,Fall
}
public class FSM_Knight : MonoBehaviour
{
    private Dictionary<StateType, State> allSaveState;
    private State currentState;
    public Animator anim;
    public Rigidbody2D rb;
    public Collider2D coll;
    public SpriteRenderer render;

    public LayerMask ground;
    public float speed;
    public int jumpForce;

    public bool spacePress;
    private void Awake()
    {
        allSaveState = new Dictionary<StateType, State>();
        AddState(StateType.Idle, new Idle_Knight(this));
        AddState(StateType.Run, new Run_Knight(this));
        AddState(StateType.Jump,new Jump_Knight(this));
        AddState(StateType.Fall,new Fall_Knight(this));
    }
    void Start()
    {
        anim=GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        render = GetComponent<SpriteRenderer>();
        ChangeState(StateType.Idle);
    }

    private void Update()
    {
        KeyCheck();
    }
    void FixedUpdate()
    {
        currentState?.OnUpdate();
        Debug.Log(currentState);
    }

    //Ìí¼Ó×´Ì¬
    private void AddState(StateType type,State state)
    {
        if (allSaveState.ContainsKey(type)) return;
        allSaveState.Add(type, state);
    }

    //ÇÐ»»×´Ì¬
    public void ChangeState(StateType nextState)
    {
        if (currentState == allSaveState[nextState]) return;
        currentState?.OnExit();
        currentState = allSaveState[nextState];
        currentState?.OnEnter();
    }

    //°´¼ü¼ì²â
    public void KeyCheck()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
            spacePress=true;
    }

    //½ÇÉ«ÒÆ¶¯
    public float Move()
    {
        float move = Input.GetAxisRaw("Horizontal");
        if (move == -1) render.flipX = true;
        else if (move == 1) render.flipX = false;
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
        return move;
    }
}
