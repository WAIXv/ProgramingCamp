using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerc : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float v = 400f;
    public float jumpforce;
    private Animator anim;
    public Collider2D coll;
    public LayerMask ground;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        falljudge();
    }
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        if (facedirection != 0)//³¯Ïò
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        if (horizontalmove != 0)//¿ØÖÆÒÆ¶¯
        {
            rb.velocity = new Vector2(horizontalmove * v, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(horizontalmove));
        }
        if (Input.GetButtonDown("Jump"))//ÌøÔ¾
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce );
            anim.SetBool("jumping", true);
        }
        }
    void falljudge()
    {
        anim.SetBool("idel", false);
        if (anim.GetBool("jumping") && rb.velocity.y <= 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }else if(coll.IsTouchingLayers(ground)){
            anim.SetBool("falling", false);
            anim.SetBool("idel", true);
        }
    }
}
