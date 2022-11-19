using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;
    public LayerMask ground;
    private Animator anim;
    public Collider2D coll;
    public int Cherry = 0;
    public int gem = 0;
    private bool isHurt;//默认是false
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(!isHurt)
        Movement();
        SwitchAnim();
    }

    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        //角色移动
        if(horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime,rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(horizontalmove));
        }

        if(facedirection != 0)//控制角色朝向
        {
            transform.localScale = new Vector3(facedirection,1,1);
        }
        //角色跳跃
        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpforce * Time.deltaTime);
            anim.SetBool("jumping",true);
        }
        //角色爬行
        if(Input.GetKey(KeyCode.X))
        {
            anim.SetBool("crouching",true);
        }
        else
        {
            anim.SetBool("crouching",false);
        }
       
    }

    void SwitchAnim()
    {
        anim.SetBool("idle",false);
        if(rb.velocity.y<1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling",true);
        }

        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        else if(isHurt)
        { 
            anim.SetBool("hurting",true);
            if(Mathf.Abs(rb.velocity.x) < 0.1)
            {
                anim.SetBool("hurting",false);
                anim.SetBool("idle",true);
                anim.SetFloat("running",0);
                isHurt = false;
            }
        }
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling",false);
            anim.SetBool("idle",true);
        }
    }
    //收集物品
    public Text CherryNum;
    public Text GemNum;
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Collection1")
        {
            Destroy(collision.gameObject);
            Cherry++;
            CherryNum.text = Cherry.ToString();
        }

        if(collision.tag == "Collection2")
        {
            Destroy(collision.gameObject);
            gem++;
            GemNum.text = gem.ToString();
        }
    }

    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(anim.GetBool("falling"))
            {
                Destroy(collision.gameObject);
                 rb.velocity = new Vector2(rb.velocity.x,jumpforce * Time.deltaTime);
                anim.SetBool("jumping",true);
            }
            else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                isHurt = true;
                rb.velocity = new Vector2(-10,rb.velocity.y);
            }
            else if(transform.position.x > collision.gameObject.transform.position.x)
            {
                isHurt = true;
                rb.velocity = new Vector2(10,rb.velocity.y);
            }
        }
    }

}
