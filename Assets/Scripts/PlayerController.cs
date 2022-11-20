using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D Rb;
    private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Collider2D coll;
    [SerializeField] private int Cherry = 0;
    [SerializeField] private int Gem = 0;
    [SerializeField] private Text CherryNum;
    [SerializeField] private Text GemNum;
    private bool ishurt;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    
    
    void Update()
    {
        if (!ishurt)
        {
            Movement();
        }
        SwitchAnim();
    }
    //角色控制
    void Movement()
    {    
        float horizontalmove= Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        //角色移动
        if (horizontalmove != 0)
        {
            Rb.velocity = new Vector2(horizontalmove * speed, Rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(facedirection));
        }
        //角色转向
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        //角色跳跃
        if((Input.GetButtonDown("Jump"))&& coll.IsTouchingLayers(ground))
        {
            Rb.velocity=new Vector2(Rb.velocity.x,jumpforce);
            anim.SetBool("idie", false);
            anim.SetBool("jumping",true);
        }
        //角色俯身
        if(Input.GetButtonDown("Vertical"))
        {
            anim.SetBool("verticaling",true);
        }
    }
    //动画效果
    void SwitchAnim()
    {
        anim.SetBool("idie",true);
        if(anim.GetBool("jumping"))
        {
            if (Rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(ishurt)
        {
            anim.SetBool("hurting", true);
            if(Mathf.Abs(Rb.velocity.x)<0.1f)
            {
                ishurt = false;
                anim.SetBool("hurting", false);
                anim.SetBool("idie", true);
            }
        }
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idie", true);
        }
    }
    //物品收集
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag=="Collection")
        {
            Destroy(collision.gameObject);
            Cherry = Cherry + 1;
            CherryNum.text = Cherry.ToString();
        }
        
        if (collision.tag == "CollectionGem")
        {
            Destroy(collision.gameObject);
            Gem = Gem + 1;
            GemNum.text = Gem.ToString();
        }
    }
    //碰到敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            if (anim.GetBool("falling"))
            {
            Destroy (collision.gameObject);
            Rb.velocity = new Vector2(Rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
            }
        else if(transform.position.x<collision.gameObject.transform.position.x)
            {
                Rb.velocity = new Vector2(-10, Rb.velocity.y);    
                ishurt = true;
            }
        else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                Rb.velocity = new Vector2(10, Rb.velocity.y);
                ishurt = true;
            }
    }

}


