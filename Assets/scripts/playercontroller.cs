using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();
        SwitchAnim();
    }
    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        //ÒÆ¶¯
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }


        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        //ÌøÔ¾
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }
    }
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
            if(anim.GetBool("jumping"))
            {
                if(rb.velocity.y<0)
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
