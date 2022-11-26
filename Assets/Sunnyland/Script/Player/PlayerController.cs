using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public LayerMask ground;    //����ͼ��
    public Text score;
    public Text HP;
    public Collider2D disColl;
    public Transform cellingCheck,groundCheck;
    public float speed;         //�ٶȱ���
    public float JumpForce;     //��Ծ�ٶ�

    private Rigidbody2D rd;
    private Animator anim;
    private Collider2D coll;
    private int cherry = 0;          //�Ե���ӣ������
    private int life = 3;               //HP
    private bool CanJump = true;        //�����ж��ܷ���Ծ
    private bool isHurt = false;        //�ж��Ƿ�����
    private bool isPast = false;        //ʱ�մ�Խ �ж��Ƿ��ڹ�ȥ 
    private bool isGround;
    private int extraJump;               //����Ծ����
    
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();   
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHurt)
        {
            Movement();
        }
        SwitchAnimation();
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        Jump();
    }

    void Movement()
    {
        float HorizontalSpeed = Input.GetAxis("Horizontal") * speed;        //������Time.deltaTime��������˲�ƣ�
        float FacedDirection = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("running", Mathf.Abs(FacedDirection));
        if (FacedDirection != 0)                   //ˮƽ�ƶ�
        {
            rd.velocity = new Vector2(HorizontalSpeed, rd.velocity.y);
            if (HorizontalSpeed < 0)                         //��������ת��
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (HorizontalSpeed > 0)                         //��������ת��
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            rd.velocity = new Vector2(0, rd.velocity.y);
        }
        /*if (Input.GetButtonDown("Jump") && CanJump)                 //�ɰ���Ծ
        {
            rd.velocity = new Vector2(rd.velocity.x, JumpForce);
            anim.SetBool("jumping", true);
            CanJump = false;
        }*/
        if (!isPast)
        {
            if (Input.GetKeyDown("q"))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + 51);
                isPast = true;
            }
        }
        else if (isPast)
        {
            if (Input.GetKeyDown("q"))
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 51);
                isPast = false;
            }
        }
        if (!Physics2D.OverlapCircle(cellingCheck.position,0.2f,ground)) 
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                anim.SetBool("crouch", true);                       //�л����¶���  ����bug�����ֶ���״̬ԭ���� ��غ��޷��ٴ���Ծ ����������ȥ����ǽ������ٴ���Ծ
                rd.velocity = new Vector2(0.5f * HorizontalSpeed, rd.velocity.y);
                disColl.enabled = false;                            //�ر��ϰ�����ײ��
            }
            else
            {
                anim.SetBool("crouch", false);                      //�г����¶���
                disColl.enabled = true;                             //���ϰ�����ײ��
            }
        }


    }
        void SwitchAnimation()
     {
             if (anim.GetBool("jumping") && rd.velocity.y < 0)       //����
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
                CanJump = false;
            }
            else if (anim.GetBool("falling") && coll.IsTouchingLayers(ground))         //���
            {
                anim.SetBool("falling", false);
                anim.SetBool("idle", true);
                CanJump = true;
            }
            if (isHurt && Mathf.Abs(rd.velocity.x) < 1)     //�������˶���
            {
                isHurt = false;
                anim.SetBool("hurt", false);
            }      
    }
    private void OnTriggerEnter2D(Collider2D collision)             
    {
        if (collision.tag == "Collection")               //����ɫ��������Ʒ
        {
            Destroy(collision.gameObject);                          //�Ե���Ʒ
            cherry++;
            score.text = cherry.ToString();
        }
        if (collision.tag == "DeadTrigger")                         //��ɫ���� ���¿�ʼ
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart",0.4f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)      
    {
        if(collision.gameObject.tag == "Enemy")             //�������⣺����ʱ��������˶���ֻ����һ��  �ѽ����any state�Ķ��������������� ������ ����ֻ����һ��
        {
            if(anim.GetBool("falling"))         //����״̬
            {
                Destroy(collision.gameObject);      //�������
                rd.velocity = new Vector2(rd.velocity.x, JumpForce);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                isHurt = true;
                anim.SetBool("hurt", true);
                rd.velocity = new Vector2(-6, rd.velocity.y);               //���������˺󷴵�
                life--;                             //HP��һ
                HP.text = life.ToString();
                if (life == 0)                      //��ɫ�������¿�ʼ
                {
                    Invoke("Restart", 0.2f);
                }
            }
            else if(transform.position.x > collision.gameObject.transform.position.x)
            {
                isHurt = true;
                anim.SetBool("hurt", true);
                rd.velocity = new Vector2(6, rd.velocity.y);                //���������˺󷴵�
                life--;                             //HP��һ
                HP.text = life.ToString();
                if(life == 0)                       //��ɫ�������¿�ʼ
                {
                    Invoke("Restart", 0.2f);
                }
            }
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Jump()
    {
        if(isGround)
        {
            extraJump = 1;
        }
        if(Input.GetButtonDown("Jump") && extraJump > 0)
        {
            rd.velocity = Vector2.up * JumpForce;
            extraJump--;
            anim.SetBool("jumping",true);
        }
        
    }
}
