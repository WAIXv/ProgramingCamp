using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controler : MonoBehaviour
{
 
    [SerializeField] public static int player_blood;
    [SerializeField] public static int ammo_amount;
    [SerializeField] public static int stair_amount;
    


    [SerializeField] private float jump_counter;
    [SerializeField] private float jump_time;
    
    
    [SerializeField] private bool able_to_attack;
    [SerializeField] private bool able_to_move;
    [SerializeField] private bool able_to_jump;
    
    
    
    [SerializeField] private float hurt_timecounter; 
    [SerializeField] private float hurt_foces;
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
        if(hurted)
        {
            hurt_timecounter+=Time.fixedDeltaTime;
        } 
        if(able_to_move)
        {
            player_move();
        }
        if(!player_feet.IsTouchingLayers(ground))
        {
            jump_counter+=Time.fixedDeltaTime;
        }
    }
    private void player_move()
    {
        player_animator.SetBool("croching",false);
        
        float player_run_direction = Input.GetAxisRaw("Horizontal");
        float player_move = Input.GetAxis("Horizontal");
        //跑动
        if(player_move!=0)
        {
            player_body.velocity = new Vector2(player_move*speedx*Time.deltaTime,player_body.velocity.y);
            player_animator.SetFloat("running",Mathf.Abs(player_run_direction));
        }
        //方向
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

        if(jump_direction>0&&able_to_jump)
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
            jump_counter = 0f;
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
    public void gethurt(int damage,bool direction)
    {
        able_to_move = false;
        hurted = true;
        audio_manager.hurted();
        print("opps");
        if(!direction)
        {
            player_body.velocity = new Vector2(-hurt_foces *Time.deltaTime,hurt_foces *Time.deltaTime); 
        }
        else
        {
            player_body.velocity = new Vector2(hurt_foces *Time.deltaTime,hurt_foces*Time.deltaTime);
        }
        if((player_blood-=damage)>0)
        {
            hurt_timecounter=0;
            player_animator.SetBool("hurt",true);
        }
        else
        {
            Dead();
        }
    }

    public void check_if_movable()
    {
        if(hurt_timecounter>=0.5f)
        {
            able_to_move = true;
        }
        if(hurt_timecounter>=1f)
        {
            able_to_attack = true;
        }
        if(jump_counter<jump_time&&Input.GetAxis("Vertical")<1f)
        {
            able_to_jump = true;
        }
        else
        {
            able_to_jump = false;
        }
    }
    public void jump()
    {
        able_to_jump =false;
        jump_counter = jump_time;
        player_body.velocity = new Vector2(player_body.velocity.x,jumpfoces);
        player_animator.SetBool("jumping",true);
    }
}
