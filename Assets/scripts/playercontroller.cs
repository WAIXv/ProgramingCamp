using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rb;
    [SerializeField] private Animator Anim;
    [SerializeField] private float Speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private Collider2D Coll;
    [SerializeField] private int Cherry = 0;
    [SerializeField] private Text cherryNum;
    [SerializeField] private bool isHurt;
    [SerializeField] private AudioSource jumpAudio,hurtAudio,cherryAudio;


    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        if (!isHurt)
        { movement(); }
    }
    void FixedUpdate()
    {
        if (!isHurt)
        { movement(); }
        SwitchAnim();
    }
    void movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");
        //�ƶ�
        if (horizontalMove != 0)
        {
            Rb.velocity = new Vector2(horizontalMove * Speed * Time.deltaTime, Rb.velocity.y);
            Anim.SetFloat("running", Mathf.Abs(faceDirection));
        }


        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
        //��Ծ
        if (Input.GetButtonDown("Jump")&&Anim.GetBool("idle"))
        {
            jumpAudio.Play(); 
            Rb.velocity = new Vector2(Rb.velocity.x, jumpForce * Time.fixedDeltaTime);
                Anim.SetBool("jumping", true); 
        }

    }

    void SwitchAnim()
    {
        Anim.SetBool("idle", false);
        if (Anim.GetBool("jumping"))
        {
            if (Rb.velocity.y < 0)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            Anim.SetBool("hurt", true);
            Anim.SetFloat("running", 0);
            Anim.SetBool("jumping", false);
            Anim.SetBool("falling", false);        
            if (Mathf.Abs(Rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                Anim.SetBool("hurt", false);
                Anim.SetBool("idle", true);
            }
        }
        else if (Coll.IsTouchingLayers(Ground))
        {
            Anim.SetBool("falling", false);
            Anim.SetBool("idle", true);
        }
        else if (!Coll.IsTouchingLayers(Ground))
        {
            Anim.SetBool("falling", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="collection")
        {
            cherryAudio.Play();
            Destroy(collision.gameObject);
            Cherry += 1;
            cherryNum.text = Cherry.ToString();
        }
        if(collision.tag=="deadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("reStart", 2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //enemy_frog frog = collision.gameObject.GetComponent<enemy_frog>();
            enemy Enemy = collision.gameObject.GetComponent<enemy>();
            if (Anim.GetBool("falling"))
            {
                //frog.Jumpon();
                Enemy.Jumpon();
                Rb.velocity = new Vector2(Rb.velocity.x, jumpForce * Time.fixedDeltaTime);
                Anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                hurtAudio.Play();
                Rb.velocity = new Vector2(-10, Rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                hurtAudio.Play();
                Rb.velocity = new Vector2(10, Rb.velocity.y);
                isHurt = true;
            }
        }
    }


    public void reStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
