using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    [SerializeField] private AudioSource touchAudio;//��ײʱ��ʵʱ����Ч
    [SerializeField] private AudioSource jumpAudio;//��Ծʱ����Ч
    [SerializeField] private int acornNum;//��ʵ����
    [SerializeField] private float speed;//��·�ٶ�
    [SerializeField] private float jumpSpeed;//��Ծ�ٶ�
    [SerializeField] private Text acorn;
    private Rigidbody2D myRigidbody;//��ȡ��Ҹ���
    private Animator myAnime;//��ȡ������
    private BoxCollider2D myFeet;//��ȡ��ײ��
    private bool isGround;//�ж��Ƿ�Ӵ��������Ϣ
    private bool canDoubleJump;//�ж��ܷ������
    private bool isHurt;//�ж��Ƿ�����
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnime = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        if (!isHurt)
        {
            run();
        }
        flip();
        jump();
        SwitchAnimation();

    }
    /// <summary>
    /// �ж��Ƿ�Ӵ�����
    /// </summary>
    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
    /// <summary>
    /// ת��
    /// </summary>
    void flip()
    {
        bool playerAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    /// <summary>
    /// ��
    /// </summary>
    void run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * speed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool playerAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnime.SetBool("run", playerAxisSpeed);
    }
    /// <summary>
    /// ��
    /// </summary>
    void jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnime.SetBool("jump", true);
                canDoubleJump = true;
                jumpAudio.Play();
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
            }
            else if (canDoubleJump)
            {
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                jumpAudio.Play();
                canDoubleJump = false;
            }


        }
    }
    /// <summary>
    /// �����л�
    /// </summary>
    void SwitchAnimation()
    {
        if (myAnime.GetBool("jump"))
        {
            if (myRigidbody.velocity.y == 0.0f)
            {
                myAnime.SetBool("jump", false);
            }
        }
        if (isHurt)
        {
            if (Mathf.Abs(myRigidbody.velocity.x) < 2f)
            {
                isHurt = false;
                myAnime.SetBool("hurt", false);
            }
        }
    }
    /// <summary>
    /// �ж��Ƿ����ʵ��ײ
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "healthPack")
        {
            touchAudio.Play();
            Destroy(collision.gameObject);
            acornNum += 1;
            acorn.text = acornNum.ToString();

        }
    }
    /// <summary>
    /// �ж��Ƿ��������ײ
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ant")
        {
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                myRigidbody.velocity = new Vector2(-10, myRigidbody.velocity.y);
                isHurt = true;
                myAnime.SetBool("hurt", true);
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                myRigidbody.velocity = new Vector2(10, myRigidbody.velocity.y);
                isHurt = true;
                myAnime.SetBool("hurt", true);
            }
        }
    }
}


