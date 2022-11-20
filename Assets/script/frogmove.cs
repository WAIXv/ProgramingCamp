using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;

public class frogmove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator Anim;
    public float jumpForce=2;
    public LayerMask ground;
    [SerializeField] public Transform leftpoint, rightpoint;
    [SerializeField] public float speed;
    [SerializeField] private bool faccLeft = true;
    private float leftx, rightx;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        //transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        SwitchAnim();

    }
    void moveMent()
    {
        if (faccLeft)
        {
            if (coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }

            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faccLeft = false;

            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }

            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faccLeft = true;
            }
        }
    }
    private void SwitchAnim()
    {

        if (Anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1f)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if (coll.IsTouchingLayers(ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);

        }
       
    }
}
