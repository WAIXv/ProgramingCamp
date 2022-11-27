using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : Enemy
{
    public Transform leftPos;
    public Transform rightPos;
    //public int damage;

    private Rigidbody2D rb;
    private PlayerHealth playerHealth;
    private float moveSpeed = 3;
    private float jumpSpeed = 10;
    private float leftX;
    private float rightX;
    private bool faceLeft = true;
    private bool isGround;

    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        transform.DetachChildren();
        leftX = leftPos.position.x;
        rightX = rightPos.position.x;
        Destroy(leftPos.gameObject);
        Destroy(rightPos.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        SwitchAnim();
    }

    void Move()
    {
        if (faceLeft)
        {
            Vector2 frogVel = new Vector2(-moveSpeed, rb.velocity.y);//�ٶ�
            rb.velocity = frogVel;
            if (transform.position.x < leftX)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                rb.velocity = new Vector2(-rb.velocity.x, 0);
                faceLeft = false;
            }
        }
        else
        {
            Vector2 frogVel = new Vector2(moveSpeed, rb.velocity.y);//�ٶ�
            rb.velocity = frogVel;
            if (transform.position.x > rightX)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                rb.velocity = new Vector2(-rb.velocity.x, 0);
                faceLeft = true;
            }
        }
        if (isGround)//��Ծ
        {
            anim.SetBool("Jump", true);//���Ŷ���
            Vector2 jumpVel = new Vector2(rb.velocity.x, jumpSpeed);//��Ծ�ٶ�
            rb.velocity = jumpVel;
        }
    }

    void CheckGrounded()//����Ƿ�Ӵ�����
    {
        isGround = rb.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void SwitchAnim()//�����л�
    {
        anim.SetBool("Idle", false);

        if (rb.velocity.y < 0.0f && !isGround)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", true);
        }
        else if (isGround)//���
        {
            anim.SetBool("Fall", false);
            anim.SetBool("Idle", true);
        }
    }

   /* void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.DamagePlayer(damage);
        }
    }*/
}
