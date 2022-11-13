using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    [SerializeField] private  Rigidbody2D rb;
    [SerializeField] private float speedx;
    [SerializeField] private float jumpforce;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();
    }

    //½ÇÉ«ÒÆ¶¯
    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        

        if  (horizontalmove != 0)
        {
            rb.velocity = new Vector2 (horizontalmove*speedx*Time.fixedDeltaTime, rb.velocity.y);
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        if (Input.GetButton("Jump"))
        {
            rb.velocity = new Vector2 (rb.velocity.x, jumpforce*Time.fixedDeltaTime);   
        }
    }
}
