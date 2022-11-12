using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public GameObject Player;
    public float speed = 5f;
    public float jumps = 6f;
    private Rigidbody2D rb;
    private Animator anmi;
    public LayerMask ground;
    public Collider2D coll;
    public int nTimesJump = 1;
    public int templ=1;
    public int Cherry;
    public Text CherryNum;
    public int hurtspeedx;
    public int hurtspeedy;
    public bool hurting;
    private  int ycjump;
    public int tempycj;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anmi = GetComponent<Animator>();
    }
    void Update()
    {
        
        if (!hurting) { 
            Movement();
    }
        Switchanmi();
    }
    void FixedUpdate()
    {
        if (!coll.IsTouchingLayers(ground)&&ycjump>0) ycjump--;
        if (coll.IsTouchingLayers(ground))
        {
            nTimesJump = templ;
            ycjump = tempycj;
        }
    }
    void Movement()
    {
        float Horizontalmove = Input.GetAxis("Horizontal");
        float face = Input.GetAxisRaw("Horizontal");
        if (Horizontalmove != 0)
        {
            rb.velocity = new Vector2(Horizontalmove * speed, rb.velocity.y);
            anmi.SetFloat("running", Mathf.Abs(Horizontalmove));
        }
        if (face != 0)
        {
            transform.localScale = new Vector3(face, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {

            if (coll.IsTouchingLayers(ground)||ycjump!=0)
            {
                rb.velocity = Vector2.up * jumps;
                anmi.SetBool("jumping", true);
                nTimesJump = templ;
            }
            else if (nTimesJump > 0)
            {
                rb.velocity = Vector2.up * jumps;
                anmi.SetBool("jumping", true);
                nTimesJump--;
            }
        }
    }
    void Switchanmi()
    {
        anmi.SetBool("idel", false);
        if (anmi.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anmi.SetBool("jumping", false);
                anmi.SetBool("falling", true);
            }
        } else if (coll.IsTouchingLayers(ground))
        {
            anmi.SetBool("falling", false);
            anmi.SetBool("idel", true);
        }
        if (hurting && Mathf.Abs(rb.velocity.x) < 2f)
        {
            anmi.SetBool("hurting", false); 
            hurting = false;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Cherry")
    //    {
    //        Destroy(collision.gameObject);
    //        /*Cherry++;
    //        CherryNum.text = Cherry.ToString();*/
    //        Scoreplus();
    //    }
    //}
    //public void Scoreplus()
    //{
    //        Cherry++;
    //        CherryNum.text = Cherry.ToString();
    //}
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Frog")
        { 
            anmi.SetBool("hurting", true);
            hurting = true;
            if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-1 * hurtspeedx, hurtspeedy);
                
            }
            if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(1 * hurtspeedx, hurtspeedy);
            }
            
        }
    }*/
    /*public void hurtingcheck(GameObject frog)
    {
        anmi.SetBool("hurting", true);
        hurting = true;
        if (transform.position.x < frog.transform.position.x)
        {
            rb.velocity = new Vector2(-1 * hurtspeedx, hurtspeedy);

        }
        if (transform.position.x > frog.transform.position.x)
        {
            rb.velocity = new Vector2(1 * hurtspeedx, hurtspeedy);
        }
    }*/
}
