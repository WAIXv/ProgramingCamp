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

    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject grabHand;
    public LayerMask groundLayer;
    public float speed;
    public int jumpForce;

    public bool spacePress;
    public bool isGround;
    public bool isOnWall;

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
        PhysicsCheck();
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

    //ÎïÀí¼ì²â
    public void PhysicsCheck()
    {
        RaycastHit2D leftFootHit;
        RaycastHit2D rightFootHit;
        RaycastHit2D grabHandHit;
        leftFootHit = Physics2D.Raycast(leftFoot.transform.position, Vector3.down, 0.1f, groundLayer);
        rightFootHit = Physics2D.Raycast(rightFoot.transform.position, Vector3.down, 0.1f, groundLayer);
        grabHandHit = Physics2D.Raycast(grabHand.transform.position, transform.right * transform.localScale.x, 0.1f, groundLayer);
        isGround = leftFootHit || rightFootHit;
        isOnWall = grabHandHit;
        Debug.DrawRay(leftFoot.transform.position, Vector3.down * 0.1f, Color.red);
        Debug.DrawRay(rightFoot.transform.position, Vector3.down * 0.1f, Color.red);
        Debug.DrawRay(grabHand.transform.position, transform.right * transform.localScale.x * 0.1f, Color.red);
    }

    //½ÇÉ«ÒÆ¶¯
    public float Move()
    {
        float moveDir = Input.GetAxisRaw("Horizontal");
        if (moveDir != 0) transform.localScale = new Vector3(moveDir, 1, 1);
        rb.velocity = new Vector2(speed * moveDir, rb.velocity.y);
        return moveDir;
    }
}
