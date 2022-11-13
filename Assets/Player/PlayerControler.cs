using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D coll;
    public float speed;
    public float jumpforce;
    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        
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

        //½ÇÉ«ÒÆ¶¯
        if(horizontalmove!=0)
        {
            rb.velocity = new Vector2(horizontalmove * speed, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        
        if(facedirection!=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        //½ÇÉ«ÌøÔ¾
        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
        }
    }

    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }


}
