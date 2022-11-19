using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bluemouse : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform leftpoint, rightpoint;
    [SerializeField] private bool faceleft = true;
    [SerializeField] private float speed;
    [SerializeField] private Animator animi;
    
    void Start()
    {
        transform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        movement();

    }
    void movement()
    {
        if (faceleft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if(transform.position.x < leftpoint.position.x)
            {
                transform.localScale = new Vector3(-transform.localScale.x , 1, 1);
                faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed,rb.velocity.y);
            if (transform.position.x > rightpoint.position.x)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
                faceleft = true;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animi.Play("bluemouse move");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animi.Play("bluemouse move");
        }
    }
}
