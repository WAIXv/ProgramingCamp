using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D feet;
    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Run();
        CheckGrounded();
        Jump();
        SwitchAnim();
    }

    void Flip()//ˮƽ��ת
    {
        bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;//x�����ٶ�
        if (playerHasXAxisSpeed)
        {
            if (rb.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);//Ĭ�ϲ���ת
            }
            if (rb.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");//����
        Vector2 playerVel = new Vector2(moveDir * runSpeed, rb.velocity.y);//�ٶ�
        rb.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;//x�����ٶ�
        anim.SetBool("Run", playerHasXAxisSpeed);//�����л�
    }
    void CheckGrounded()//����Ƿ�Ӵ�����
    {
        isGround = feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            anim.SetBool("Jump", true);//���Ŷ���
            Vector2 jumpVel = new Vector2(rb.velocity.x, jumpSpeed);//��Ծ�ٶ�
            rb.velocity = jumpVel;
        }
    }

    void SwitchAnim()//�����л�
    {
        anim.SetBool("Idle", false);
        if (anim.GetBool("Jump"))//����
        {
            if (rb.velocity.y < 0.0f)
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Fall", true);
            }
        }
        else if (isGround)//���
        {
            anim.SetBool("Fall", false);
            anim.SetBool("Idle", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//�ȵ����˺�����
    {
        if (anim.GetBool("Fall") && collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool("Jump", true);//���Ŷ���
            Vector2 jumpVel = new Vector2(rb.velocity.x, jumpSpeed);//��Ծ�ٶ�
            rb.velocity = jumpVel;
        }
    }
}
