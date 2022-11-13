using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public int jumpNumber;

    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myfeet;
    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myfeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Flip();
        Jump();
        CheckGround();
        SwitchAnimation();
    }

    void CheckGround()
    {
        isGround = myfeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        Debug.Log(isGround);
    }

    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playVel;

        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("IsRunning", playerHasXAxisSpeed);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") & jumpNumber < 1)
        {
            myAnim.SetBool("IsJumping", true);
            Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
            myRigidbody.velocity = Vector2.up * jumpVel;
            jumpNumber++;        
        }

        if (isGround)
        {
            jumpNumber = 0;
        }
    }

    void SwitchAnimation()
    {
        if(myAnim.GetBool("IsJumping") & myRigidbody.velocity.y < 0.0f)
        {
            myAnim.SetBool("IsJumping", false);
            myAnim.SetBool("IsFalling", true);
        }
        else if(isGround)
        {
            myAnim.SetBool("IsFalling", false);
        }
    }
}
