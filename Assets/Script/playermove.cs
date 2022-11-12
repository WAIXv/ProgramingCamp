using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    public Rigidbody2D fox;
    public Animator animi;
    public float speed;
    public float jumpforce;
    public LayerMask ground;
    public Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        switchanimation();
    }
    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float direction = Input.GetAxisRaw("Horizontal");
        if(horizontalmove != 0)
        {
             fox.velocity = new Vector2(horizontalmove * speed, fox.velocity.y);
            animi.SetFloat("running", Mathf.Abs(direction));
        }
        if(direction != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
        if (Input.GetButtonDown("Jump"))
        {
            fox.velocity = new Vector2(fox.velocity.x, jumpforce);
            animi.SetBool("jumping", true);
        }
    }
    void switchanimation()
    {
        animi.SetBool("idle", false);
        if (animi.GetBool("jumping"))
        {
            if (fox.velocity.y < 0)
            {
                animi.SetBool("jumping", false);
                animi.SetBool("falling", true);
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            animi.SetBool("falling", false);
            animi.SetBool("idle", true);
        }
        if(animi.GetBool("falling"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                fox.velocity = new Vector2(fox.velocity.x, jumpforce);
                animi.SetBool("falling", false);
                animi.SetBool("jumping", true);
            }
        }
    }

}
