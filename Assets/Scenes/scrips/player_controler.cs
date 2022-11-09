using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controler : MonoBehaviour
{
    public float speedx;
    public float jumpfoces;
    public Rigidbody2D player_body;
    public Collider2D player_coll;
    public LayerMask ground;
    public Animator player_animator;
    // Start is called before the first frame update
    void Start()
    {
        
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
        
        float player_face_direction = Input.GetAxisRaw("Horizontal");
        float player_move = Input.GetAxis("Horizontal");
        
        if(player_move!=0)
        {
            player_body.velocity = new Vector2(player_move*speedx*Time.deltaTime,player_body.velocity.y);
            player_animator.SetFloat("running",Mathf.Abs(player_face_direction));
        }

        if(player_face_direction!=0)
        {
           transform.localScale = new Vector3(player_face_direction,1,1);
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
}
