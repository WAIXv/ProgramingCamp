using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D Rb;
    private Animator anim;
    [SerializeField]private float speed;
    [SerializeField]private float jumpforce;
    [SerializeField]private LayerMask ground;
    [SerializeField] private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        Rb=GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SwitchAnim();
    }
    
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
        if(Input.GetButtonDown("Jump"))
        {
            Rb.velocity=new Vector2(Rb.velocity.x,jumpforce);
            anim.SetBool("jumping",true);
        }
    }
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
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idie", true);
        }
    }
}


