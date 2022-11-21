using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    private float speed=400;
    private float jumpforce=400;
    [SerializeField]private Animator anim;

    public Collider2D coll;
    public LayerMask ground;
    public int Cherry;
    public Text CherryNum;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();

    }
    void FixedUpdate()
    {
        Movement();
        SwitchAnim();
    }

    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        
        //角色移动
        if(horizontalmove!=0)
        {
            rb.velocity=new Vector2(horizontalmove*speed * Time.deltaTime,rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        if(facedirection!=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        //角色跳跃
        if(Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity=new Vector2(rb.velocity.x,jumpforce*Time.deltaTime);
            anim.SetBool("jumping", true);
        }

        
    }

    //切换动画效果
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling", true);
            }
        }
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }

    }

    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if(collision.tag == "Collection")
        {
            Destroy (collision.gameObject);
            Cherry += 1;
            CherryNum.text = Cherry.ToString();
        }
    }

    //消灭怪物
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
