using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speedx;
    public int speedy;
    public float jumpforce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }
     void movement()
    {
        float horizontalmove;
        float verticalmove;
        verticalmove = Input.GetAxis("Vertical");
        horizontalmove = Input.GetAxis("Horizontal");
        if(horizontalmove != 1 && verticalmove != 1)
        {
            rb.velocity = new Vector2(horizontalmove * speedx, verticalmove*speedy);
        }
    }
}
