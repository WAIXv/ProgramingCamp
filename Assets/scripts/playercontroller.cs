using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speedx;
    
    
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
        if  (horizontalmove != 0)
        {
            rb.velocity = new Vector2 (horizontalmove*speedx*Time.deltaTime, 0);
        }
    }
}
