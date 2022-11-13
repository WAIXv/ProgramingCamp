using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D PlayerRigidBody;
    private Animator PlayerAnimator;
    public BoxCollider2D PlayerCollider;
    public LayerMask Ground;
    public int CherryNum;
    [SerializeField]private float MoveSpeed;
    [SerializeField] private float JumpSpeed;
    float MoveDir;
    float FaceDir;
    bool Jump;
    bool LongJump;
    bool IsCourch;
    bool IsOnGround;
    bool IsSilde;
    bool IsGetonPlatform;
    Vector2 PlayerBasicCollSize;
    Vector2 PlayerBasicCollOffest;
    Vector2 PlayerCrouchCollSize;
    Vector2 PlayerCrouchCollOffest;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidBody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        PlayerCollider = GetComponent<BoxCollider2D>();
        PlayerBasicCollSize = PlayerCollider.size;
        PlayerBasicCollOffest = PlayerCollider.offset;
        PlayerCrouchCollSize = new Vector2(PlayerCollider.size.x, PlayerCollider.size.y/2f);
        PlayerCrouchCollOffest = new Vector2(PlayerCollider.offset.x, -0.7f);
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        CollisionByRay();
        Movement();
        AnimationSwitching();
    }
    void Update()
    {
        if ((int)(Time.time % 5) ==0 && Input.anyKey)
        {
            //Instantiate(ItemPerfabs, new Vector3(Random.insideUnitCircle.x * 5 + transform.position.x, Random.insideUnitCircle.y * 5 + transform.position.y, transform.position.z),transform.rotation);
        }
        MoveDir = Input.GetAxis("Horizontal");
        FaceDir = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump")) Jump = true;
        IsCourch = Input.GetButton("Courch") ? true : false;
        LongJump = Input.GetButton("Jump") ? true : false;
        IsSilde = Input.GetButton("Silde") ? true : false;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            Destroy(collision.gameObject);
            CherryNum++;
        }
    }
    public void Movement()
    {
        PlayerRigidBody.drag = IsOnGround ? 2 : 0;
        if (IsSilde)
        { 
            StartCoroutine(Silde(0.5f));
        }
        if (MoveDir != 0)
        {

            PlayerRigidBody.velocity = new Vector2(MoveDir * MoveSpeed * (IsCourch? 0.5f:1), PlayerRigidBody.velocity.y);
           
            PlayerAnimator.SetFloat("Running", Mathf.Abs(MoveDir));
        }
        if(FaceDir != 0)
        {
            transform.localScale = new Vector3(FaceDir, 1, 1);
        }
        if(Jump)
        {
            if(IsOnGround)
            {
                PlayerRigidBody.velocity += Vector2.up * JumpSpeed * (IsCourch ? 1.25f : 1);
            }
        }
        if (LongJump && !IsOnGround)
        {
            PlayerRigidBody.velocity += Vector2.up * 3.5f * Time.fixedDeltaTime;
        }

        if (IsCourch && IsOnGround)
        {
            PlayerCollider.size = PlayerCrouchCollSize;
           PlayerCollider.offset = PlayerCrouchCollOffest;
        }
        else
        {
            PlayerCollider.size = PlayerBasicCollSize;
            PlayerCollider.offset = PlayerBasicCollOffest;
        }
        if(IsGetonPlatform)
        {
            PlayerRigidBody.AddForce(Vector2.up * PlayerRigidBody.mass * 4);
        }

    }
    public void AnimationSwitching()
    {
        // 站立
        PlayerAnimator.SetBool("Idle", IsOnGround ? true : false);

        //跑动
        float MoveDir = Input.GetAxis("Horizontal");
        if (MoveDir != 0)
        {
            PlayerAnimator.SetFloat("Running", Mathf.Abs(MoveDir));
        }
        //跳跃
        if (Jump)
        {
            PlayerAnimator.SetBool("JumpUp",true);
            PlayerAnimator.SetBool("JumpDown", false);
            Jump = false;
        }
        if(PlayerAnimator.GetBool("JumpUp") && PlayerRigidBody.velocity.y < 0)
        {
                PlayerAnimator.SetBool("JumpDown", true);
                PlayerAnimator.SetBool("JumpUp", false);
        }
            if(IsOnGround)
            {
                
                PlayerAnimator.SetBool("JumpDown", false);
            }
    }
    IEnumerator Silde(float a)
    {
        PlayerRigidBody.AddForce(new Vector2(transform.localScale.x * 10, 0.1f), ForceMode2D.Force);
        yield return new WaitForSeconds(a);
    }
    public void CollisionByRay()
    {
        Vector3 ColliderOffest = new Vector3(PlayerCollider.offset.x, PlayerCollider.offset.y, 0);
        //检测是否在地面
        RaycastHit2D onGround = Physics2D.Raycast(transform.position + ColliderOffest, Vector2.down, PlayerCollider.size.y * 0.75f, Ground);
        Debug.DrawRay(transform.position + ColliderOffest, Vector2.down * PlayerCollider.size.y * 0.75f, Color.red,0.001f);
        IsOnGround = (onGround) ? true : false;

        //检测是否贴近墙面，并身体上方高于平台
        RaycastHit2D FixedClosedWall = Physics2D.Raycast(transform.position + ColliderOffest - new Vector3(0, PlayerCollider.size.y *0.3f, 0), Vector2.right * transform.lossyScale.x, 1, Ground);
        Debug.DrawRay(transform.position + ColliderOffest - new Vector3(0, PlayerCollider.size.y * 0.3f, 0), Vector2.right * transform.lossyScale.x, Color.red, 0.001f);
        RaycastHit2D Headhigher = Physics2D.Raycast(transform.position + ColliderOffest - new Vector3(0,PlayerCollider.size.y/2f,0), Vector2.right * transform.lossyScale.x, 1, Ground);
        Debug.DrawRay(transform.position  + ColliderOffest + new Vector3(0,-PlayerCollider.size.y/2f,0), Vector2.right * transform.lossyScale.x, Color.red, 0.001f);
        IsGetonPlatform = ((!FixedClosedWall && Headhigher && PlayerRigidBody.velocity.y >0 && !IsOnGround) ? true : false);
        if(IsGetonPlatform)
        {
            print("1");
        }
    }
}
