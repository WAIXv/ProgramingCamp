using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class playermove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D fox;
    [SerializeField] Animator animi;
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Collider2D coll;
    [SerializeField] private int cherry;
    [SerializeField] private bool isHurt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurt)
        {
            movement();
        }
        switchanimation();
    }
    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float direction = Input.GetAxisRaw("Horizontal");
        if (horizontalmove != 0)
        {
            fox.velocity = new Vector2(horizontalmove * speed, fox.velocity.y);
            animi.SetFloat("running", Mathf.Abs(direction));
        }
        if (direction != 0)
        {
            transform.localScale = new Vector3(direction, 1, 1);
        }
        if (Input.GetButtonDown("Jump") && animi.GetBool("idle"))
        {
            fox.velocity = new Vector2(fox.velocity.x, jumpforce);
            animi.SetBool("jumping", true);
        }

    }
    void switchanimation()
    {
        animi.SetBool("idle", false);
        if (isHurt)
        {
            animi.Play("hurt");
            if (Mathf.Abs(fox.velocity.x) < 0.01f)
            {
                isHurt = false;
                animi.Play("idle");
            }
        }
        if(fox.velocity.y < 0 && !coll.IsTouchingLayers(ground))
        {
            animi.SetBool("falling", true);
        }
        if (animi.GetBool("jumping"))
        {
            if (fox.velocity.y < 0)
            {
                animi.SetBool("jumping", false);
                animi.SetBool("falling", true);
            }
        }
        if (animi.GetBool("falling"))
        {
            if (Input.GetButtonDown("Jump") && animi.GetBool("idle"))
            {
                fox.velocity = new Vector2(fox.velocity.x, jumpforce);
                animi.SetBool("falling", false);
                animi.SetBool("jumping", true);
            }
        }
        if (coll.IsTouchingLayers(ground))
        {
            animi.SetBool("falling", false);
            animi.SetBool("idle", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            cherry++;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            if (animi.GetBool("runing"))
            {
                animi.SetBool("idle", true);
                animi.SetBool("runing", false);
            }
            if (animi.GetBool("falling"))
            {
                Destroy(collision.gameObject);
                fox.velocity = new Vector2(fox.velocity.x, jumpforce);
                animi.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                fox.velocity = new Vector2(-6, fox.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                fox.velocity = new Vector2(6, fox.velocity.y);
                isHurt = true;
            }
        }
    }

}
