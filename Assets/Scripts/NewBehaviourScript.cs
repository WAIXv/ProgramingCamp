using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private  Animator anim;
    public Collider2D coll;
    public Collider2D DisColl;
    public Transform CellingCheck, GroundCheck;
    public AudioSource jumpAudio;
    public AudioSource hurtAudio;
    public AudioSource cherryAudio;
    public float speed = 10f;
    public float jumpForce;
    public LayerMask ground;
    [SerializeField]public int Cherry = 0;
    public Text CherryNum;
    private bool isHurt;
    private bool checkhead = false;
    private bool isGround;
    private int extraJump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, ground);
    }
    private void Update()
    {
        //Jump();
        newJump();
        Crouch();
        CherryNum.text = Cherry.ToString();
    }
    void Movement()
    {
        float horizontalMove=Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(faceDirection));
        }
        if(faceDirection!= 0) 
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
    }
    void SwitchAnim()
    {
        //anim.SetBool("idle", false);
        if(rb.velocity.y<0.1f&&!coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                //nim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            //anim.SetBool("idle", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Collection")
        {
            cherryAudio.Play();
            collision.GetComponent<Animator>().Play("feedback");
            //Destroy(collision.gameObject);
            //Cherry += 1;
            //CherryNum.text = Cherry.ToString();
        }
        if (collision.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                hurtAudio.Play();
                isHurt= true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
        }
    }
    void Crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            DisColl.enabled = false;
            anim.SetBool("crouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            checkhead = true;
        }
        if (checkhead)
        {
            if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, ground))
            {
                DisColl.enabled = true;
                anim.SetBool("crouching", false);
                checkhead = false;
            }
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /*void Jump()
    {
        if (Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
    }*/

    void newJump()
    {
        if(isGround)
        {
            extraJump = 1;
        }
        if(Input.GetButtonDown("Jump") && extraJump>0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJump--;
            anim.SetBool("jumping", true);
        }
        if(Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
        {
            rb.velocity = Vector2.up * jumpForce;
            anim.SetBool("jumping", true);
        }
    }
    public void CherryCount()
    {
        Cherry += 1;
    }
}
