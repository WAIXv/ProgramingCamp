using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playermove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Fox;
    [SerializeField] Animator animi;
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Collider2D coll;
    [SerializeField] private Transform CellingCheck;
    [SerializeField] private int cherry;
    [SerializeField] private bool isHurt;
    [SerializeField] private AudioSource JumpAudio;
    [SerializeField] private AudioSource CollectAudio;
    [SerializeField] private Text CherryNumber;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurt)
        {
            Movement();
        }
        switchanimation();
    }
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float direction = Input.GetAxisRaw("Horizontal");
        if (horizontalmove != 0)
        {
            Fox.velocity = new Vector2(horizontalmove * Speed, Fox.velocity.y);
            animi.SetFloat("running", Mathf.Abs(direction));
        }
        if (direction != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
        if (Input.GetButtonDown("Jump") && animi.GetBool("idle"))
        {
            Fox.velocity = new Vector2(Fox.velocity.x, JumpForce);
            animi.SetBool("jumping", true);
            JumpAudio.Play();
        }
        Crouch();

    }//ÒÆ¶¯
    void switchanimation()
    {
        animi.SetBool("idle", false);
        if (isHurt)
        {
            animi.Play("hurt");
            if (Mathf.Abs(Fox.velocity.x) < 0.01f)
            {
                isHurt = false;
                animi.Play("idle");
            }
        }
        if(Fox.velocity.y < 0 && !coll.IsTouchingLayers(ground))
        {
            animi.SetBool("falling", true);
        }
        if (animi.GetBool("jumping"))
        {
            if (Fox.velocity.y < 0)
            {
                animi.SetBool("jumping", false);
                animi.SetBool("falling", true);
            }
        }
        if (animi.GetBool("falling"))
        {
            if (Input.GetButtonDown("Jump") && animi.GetBool("idle"))
            {
                Fox.velocity = new Vector2(Fox.velocity.x, JumpForce);
                animi.SetBool("falling", false);
                animi.SetBool("jumping", true);
            }
        }
        if (coll.IsTouchingLayers(ground))
        {
            animi.SetBool("falling", false);
            animi.SetBool("idle", true);
        }
    }//ÇÐ»»
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ÊÕ¼¯
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            cherry++;
            CollectAudio.Play();
            CherryNumber.text = cherry.ToString();
        }
        if (collision.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 1.5f);
        }
        

    }
    
    private void OnCollisionEnter2D(Collision2D collision)//Åö×²
    {
        if (collision.gameObject.tag == "Enemies")
        {
            enemy enemy = collision.gameObject.GetComponent<enemy>();
            if (animi.GetBool("runing"))
            {
                animi.SetBool("idle", true);
                animi.SetBool("runing", false);
            }
            if (animi.GetBool("falling"))
            {
                enemy.JumpOn();
                Fox.velocity = new Vector2(Fox.velocity.x, JumpForce);
                animi.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                Fox.velocity = new Vector2(-6, Fox.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                Fox.velocity = new Vector2(6, Fox.velocity.y);
                isHurt = true;
            }
        }
    }
    void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    void Crouch()
    {
        if(!Physics2D.OverlapCircle(CellingCheck.position,0.2f,ground))
        {
            if (Input.GetButtonDown("crouch"))
            {
                animi.SetBool("crouching", true);
                coll.enabled = false;
            }
            else if (Input.GetButtonUp("crouch"))
            {
                animi.SetBool("crouching", false);
                coll.enabled = true;
            }
        }
    }
   
}

