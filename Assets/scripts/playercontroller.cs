using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    private  Rigidbody2D player;
    private Animator anim;
    [SerializeField] private Collider2D feet;
    [SerializeField] private float speedx;
    [SerializeField] private float jumpforce;
    [SerializeField] private LayerMask ground;
    [SerializeField] private int cherry = 0;
    [SerializeField] private int gem = 0;
    private int jumpchance;

    
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        movement();
        SwitchAnim();
    }

    //角色移动
    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        
        //角色移动
        if  (horizontalmove != 0)
        {
            player.velocity = new Vector2 (horizontalmove*speedx*Time.fixedDeltaTime, player.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        
        //转头
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        if (anim.GetBool("onground"))
            jumpchance = 2;
        //角色跳跃
        if (Input.GetButton("Jump")&& jumpchance > 0)
        {
            player.velocity = new Vector2 (player.velocity.x, jumpforce*Time.fixedDeltaTime);
            anim.SetBool("jumping", true);
            jumpchance -= 1;
            anim.SetBool("onground",false);
        }
    }

    //变化动画
    void SwitchAnim()
    {
        anim.SetBool("idle", false);

        if(anim.GetBool("jumping"))
        {
            if(player.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(feet.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle",true);
            anim.SetBool("onground", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collection")
        {
            Destroy(collision.gameObject);
            cherry += 1;
        }
    }


}
