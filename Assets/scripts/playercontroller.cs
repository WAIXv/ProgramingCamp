using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    [SerializeField] private  Rigidbody2D player;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D feet;
    [SerializeField] private float speedx;
    [SerializeField] private float jumpforce;
    [SerializeField] private LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        //角色跳跃
        if (Input.GetButton("Jump"))
        {
            player.velocity = new Vector2 (player.velocity.x, jumpforce*Time.fixedDeltaTime);
            anim.SetBool("jumping", true);
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
        }
    }




}
