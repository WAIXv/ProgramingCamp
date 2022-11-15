using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controler : MonoBehaviour
{
    [SerializeField] public static int player_blood;
    [SerializeField] public static int ammo_amount;
    [SerializeField] public static int stair_amount;
    
    [SerializeField] private float hurt_foces;
    [SerializeField] private bool able_to_move;
    [SerializeField] private bool hurted;
    [SerializeField] private float speedx;
    [SerializeField] private float jumpfoces;
    [SerializeField] private Rigidbody2D player_body;
    [SerializeField] private Collider2D player_head;
    [SerializeField] private Collider2D player_feet;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Animator player_animator;
    
    [SerializeField] private bool N_facing;
    // Start is called before the first frame update
    void Start()
    {
        able_to_move=true;
        player_blood=10000;
        hurted=false;
        ammo_amount=5;
        stair_amount=0;
        N_facing = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animation_change();
        check_if_movable();
         if(able_to_move)
         {
            player_move();
         }
    }
    private void player_move()
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

        if(jump_direction>0&&player_feet.IsTouchingLayers(ground))
        {
            jump();
        }
        else if(jump_direction<0&&player_feet.IsTouchingLayers(ground))
        {
            player_animator.SetBool("croching",true);
        }
    }
    private void transdirection()
    {
        transform.Rotate(0f,180f,0f);
    }

    private void animation_change()
    {
        if(player_animator.GetBool("jumping"))
        {
            if(player_body.velocity.y<=0)
            {
                player_animator.SetBool("jumping",false);
                player_animator.SetBool("falling",true);
            }
        }
        if(player_feet.IsTouchingLayers(ground))
        {
            player_animator.SetBool("jumping",false);
            player_animator.SetBool("falling",false);
            player_animator.SetBool("hurt",false);
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

    private void Dead()
    {

    }    
    public void gethurt(int damage)
    {
        able_to_move = false;
        hurted = true;
        print("opps");
        if(N_facing)
        {
            player_body.velocity = new Vector2(-hurt_foces *Time.deltaTime,hurt_foces *Time.deltaTime); 
        }
        else
        {
            player_body.velocity = new Vector2(hurt_foces *Time.deltaTime,hurt_foces*Time.deltaTime);
        }
        if((player_blood-=damage)>0)
        {
            player_animator.SetBool("hurt",true);
        }
        else
        {
            Dead();
        }
    }

    public void check_if_movable()
    {
        if(Mathf.Abs(player_body.velocity.x)<0.1)
        {
            able_to_move = true;
        }
    }
    public void jump()
    {
            player_body.velocity = new Vector2(player_body.velocity.x,jumpfoces);
            player_animator.SetBool("jumping",true);
    }
}
