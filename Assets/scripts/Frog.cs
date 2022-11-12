using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public Move squarefo;
    public LayerMask ground;
    public GameObject Frogobject;
    public Transform leftt;
    public Transform rightt;
    public float left;
    public float right;
    public Rigidbody2D rbf;
    public int xspeed;
    public int yspeed;
    public Collider2D col;
    private bool faceleft = true;
    public Transform t;
    public Animator anmi;
    void Start()
    {
        left = leftt.position.x;
        right = rightt.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        switchanmi();
    }

    //private void OnCollisionEnter2D(Collision2D colli)
    //{
    //    if (/*colli.gameObject.CompareTag("Player")&&*/colli is BoxCollider2D)
    //    {
    //        print("dsajhkjad");
    //        squarefo.hurtingcheck(Frogobject);
    //    }
    //}
    private void switchanmi()
    {
        if (rbf.velocity.y > 0) anmi.SetBool("flogjump", true);
        if (rbf.velocity.y < 0) anmi.SetBool("flogfalling", true);
        if (rbf.velocity.y < 0) anmi.SetBool("flogjump", false);
        if (col.IsTouchingLayers(ground)) anmi.SetBool("flogfalling", false);
    }
    private void Movement()
    {
        
        if (faceleft)
        {
            if (col.IsTouchingLayers(ground))  rbf.velocity = new Vector2(-xspeed, yspeed);
            if (t.position.x < left)
            {
                print(left);
                t.localScale = new Vector3(-1, 1, 1);
                faceleft = false;
            }
        }
        if (!faceleft)
        {
            if (col.IsTouchingLayers(ground)) rbf.velocity = new Vector2(xspeed, yspeed);
            if (t.position.x > right)
            {
                t.localScale = Vector3.one;
                faceleft = true;
            }
        }
    }
}
