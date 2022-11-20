using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    public Collider2D coll;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public int Cherry;
    public int Gem;

    public Text CherryNum;
    public Text GemNum;
    private bool isHurt;//默认时false
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHurt)
        {
            Movement();
        }
        SwitchAnim();
    }

    void Movement()
    {
        float horizontalmove= Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        //角色移动
        if(horizontalmove!=0)
        {
            rb.velocity = new Vector2(horizontalmove * speed, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        
        if(facedirection!=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        //角色跳跃
        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
        }


        if(Input.GetButtonDown("Vertical") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(horizontalmove * 0, rb.velocity.y);
            anim.SetBool("crouching", true);
            
        }

    }

    //切换动画效果
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if(rb.velocity.y<0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x)<0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }

        if (anim.GetBool("crouching"))
        {

            if(!Input.GetButtonDown("Vertical"))
            {
                anim.SetBool("crouching", false);
                anim.SetBool("idle", true);
            }

        }
    }

    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Collection")
        {
            
            
                Destroy(collision.gameObject);
                Cherry += 1;
                CherryNum.text = Cherry.ToString();
            
        }
        if (collision.tag == "Collection2")
        {
                anim.SetTrigger("disappear");
                Destroy(collision.gameObject);
                Gem += 1;
                GemNum.text = Gem.ToString();
            
        }
    }

    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {

          Enemy_Frog frog = collision.gameObject.GetComponent<Enemy_Frog>();
          if (anim.GetBool("falling"))
             {
                frog.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
             }
          else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }
           else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
    }
}
