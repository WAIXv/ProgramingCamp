using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controler : MonoBehaviour
{
    public static int ammo_amount;
    public static int stair_amount;
    public float speedx;
    public float jumpfoces;
    public Rigidbody2D player_body;
    public Collider2D player_coll;
    public LayerMask ground;
    public Animator player_animator;
    
    public bool N_facing;
    // Start is called before the first frame update
    void Start()
    {
        ammo_amount=5;
        stair_amount=0;
        N_facing = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         animation_change();
         player_move();   
    }
    void player_move()
    {
        player_animator.SetBool("croching",false);
        
        float player_run_direction = Input.GetAxisRaw("Horizontal");
        float player_move = Input.GetAxis("Horizontal");
        
        if(player_move!=0)
        {
            player_body.velocity = new Vector2(player_move*speedx*Time.deltaTime,player_body.velocity.y);
            player_animator.SetFloat("running",Mathf.Abs(player_run_direction));
        }

        if(player_run_direction<0&&N_facing)
        {
            N_facing=!N_facing;
            transdirection();
        }

        if(player_run_direction>0&&!N_facing)
        {
            N_facing=!N_facing;
            transdirection();
        }

        float jump_direction=Input.GetAxisRaw("Vertical");

        if(jump_direction>0&&player_coll.IsTouchingLayers(ground))
        {
            player_body.velocity = new Vector2(player_body.velocity.x,jump_direction*jumpfoces);
            player_animator.SetBool("jumping",true);
        }
        else if(jump_direction<0&&player_coll.IsTouchingLayers(ground))
        {
            player_animator.SetBool("croching",true);
        }
    }
    
    void transdirection()
    {
        transform.Rotate(0f,180f,0f);
    }

    void animation_change()
    {
        if(player_animator.GetBool("jumping"))
        {
             if(player_body.velocity.y<=0)
            {
                player_animator.SetBool("jumping",false);
                player_animator.SetBool("falling",true);
            }
        }
        if(player_coll.IsTouchingLayers(ground))
        {
            player_animator.SetBool("jumping",false);
            player_animator.SetBool("falling",false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="stair")
        {
            Destroy(other.gameObject);
            stair_amount++;
        }
        else if(other.tag=="ammo")
        {
            Destroy(other.gameObject);
            ammo_amount++;
        }
    }
}
