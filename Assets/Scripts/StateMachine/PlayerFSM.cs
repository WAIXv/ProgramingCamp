using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Parameter
{
    public float MaxMoveSpeed;
    public float RunTime;
    public float RunPercent;
    public float SpeedXInEndRun;
    public AnimationCurve RunSpeedCurve;
    public AnimationCurve SlowDownSpeedCurve;
    public float FaceDir;

    public float MaxJumpSpeed;
    public float JumpTime;
    public float JumpPercent;
    public float toyoTimer;
    public float toyoTime;
    public AnimationCurve JumpSpeedCurve;
    public bool IsJump;
    public int MaxJumpNum;
    public int JumpNum;

    public int AttackNum;
    public bool IsAttack;

    public bool IsOnGround;
    public bool AttackNext;

    public bool IsRoll;

    public int Score;

    public CapsuleCollider2D PlayerCollider;
    public BoxCollider2D GroundCollider;
    public Transform PlayerTransform;
    public Rigidbody2D PlayerRigidbody;
    public LayerMask GroundMask;
    public Animator animator;
}
public class PlayerFSM : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D PlayerCollider;
    [SerializeField] private BoxCollider2D GroundCollider;
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Rigidbody2D PlayerRigidbody;
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private LayerMask PlatformMask;
    [SerializeField] private float RestoreLayerTime;

    public Parameter parameter;
    private bool RollInFall;
    private bool IsOneWayPlatform;
    public enum PlayerStateType
    {
        Idle,Run,SlowDown,Roll,Jump,Fall,Attack1,Attack2,Attack3
    }
    private IState currentState;
    private Dictionary<PlayerStateType, IState> states = new Dictionary<PlayerStateType, IState>();
    // Start is called before the first frame update
    void Start()
    {
        parameter.animator = GetComponent<Animator>();
        parameter.PlayerRigidbody = GetComponent<Rigidbody2D>();
        parameter.PlayerTransform = GetComponent<Transform>();
        parameter.GroundCollider = GetComponent<BoxCollider2D>();
        parameter.GroundMask = GetComponent<LayerMask>();
        ReStartDictionary();
        transitionState(PlayerStateType.Idle);
    }

    // Update is called once per frame
     void Update()
    {
        parameter.FaceDir = Input.GetAxisRaw("Horizontal");
        parameter.IsJump = Input.GetButton("Jump");
        parameter.IsRoll = Input.GetKey(KeyCode.LeftControl)|| RollInFall;
        parameter.IsOnGround = CheckGournd();

        if(Input.GetKey(KeyCode.LeftControl) && currentState == states[PlayerStateType.Fall])
        {
            StartCoroutine(RollIEnumerator());
        }
        if (currentState == null)
        {
            ReStartDictionary();
            currentState = states[PlayerStateType.Idle];
        }
        currentState.OnUpdate();
        CheckOneWayPlatform();
        OneWayPlatformDownCheck();
    }
    private void FixedUpdate()
    {
        if (parameter.IsOnGround)
        {
            parameter.toyoTimer = parameter.toyoTime;
        }
        if (!parameter.IsOnGround)
        {
            parameter.toyoTimer -= Time.deltaTime;
        }
        currentState.OnFixedUpdate();
    }
    public void OnUpdate()
    {
        currentState.OnUpdate();
    }
    public void transitionState(PlayerStateType stateType)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[stateType];
        currentState.OnEnter();
    }
    private void ReStartDictionary()
    {
        states.Add(PlayerStateType.Idle, new PlayerIdle(this));
        states.Add(PlayerStateType.Run, new PlayerRun(this));
        states.Add(PlayerStateType.SlowDown, new PlayerSlowDown(this));
        states.Add(PlayerStateType.Roll, new PlayerRoll(this));
        states.Add(PlayerStateType.Jump, new PlayerJump(this));
        states.Add(PlayerStateType.Fall, new PlayerFall(this));
        states.Add(PlayerStateType.Attack1, new PlayerAttack1(this));
        states.Add(PlayerStateType.Attack2, new PlayerAttack2(this));
        states.Add(PlayerStateType.Attack3, new PlayerAttack3(this));
        
    }
    private bool CheckGournd()
    {
        return GroundCollider.IsTouchingLayers(GroundMask)|| GroundCollider.IsTouchingLayers(PlatformMask);
    }
    private void CheckOneWayPlatform()
    {
        IsOneWayPlatform = GroundCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            parameter.Score++;
            Destroy(collision.gameObject);
        }
    }
    void OneWayPlatformDownCheck()
    {
        float MoveY = Input.GetAxis("Vertical");

        if (IsOneWayPlatform && Input.GetKeyDown(KeyCode.S))
        {
            gameObject.layer = LayerMask.NameToLayer("Platform");
            Invoke("RestorePlayerlayer", RestoreLayerTime);
        }

    }
    void RestorePlayerlayer()
    {
        if(gameObject.layer != LayerMask.GetMask("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    IEnumerator RollIEnumerator()
    {
        RollInFall = true;
        yield return new WaitForSeconds(0.2f);
        RollInFall = false;
    }
}
