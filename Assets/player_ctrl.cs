using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using static Assets.EntityStates;

public class player_ctrl : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D _Rigidbody2D;
    BoxCollider2D _Collider;
    GameObject visual;

    private Animator a;

    bool onGround = false;
    float jump_cool = 0f;
    float ground_tick = 0.12f;
    float move_v = 0f;
    static float stop_a = 32.0f;
    static float scale_x;
    bool d_r = true;

    public static EntityState ROOT, WALK; //JUMP, OnAir;
    public static EntityStateMgr StateMgr = new EntityStateMgr();

    void Start()
    {
        //init
        _Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        _Collider = this.gameObject.GetComponent<BoxCollider2D>();
        visual = GameObject.Find("visual");
        scale_x = visual.transform.localScale.x;
        a = visual.GetComponent<Animator>();

        //init state 
        ROOT = StateMgr.Add(new EntityState(true,
        (gObj) => { },
        (gObj) => {
            if ((onGround || ground_tick > 0) && (Input.GetKeyDown(KeyCode.Space) || (int)Input.GetAxisRaw("Vertical") > 0))
            {
                print("jump");
                onGround = false;
                //WALK.SetActive(true);
                doJump(15.0f);
                jump_cool = 0.2f;
            }
            if (jump_cool > 0f)
            {
                jump_cool -= Time.deltaTime;
            }

            int axis_x = -(int)GetHorIn();
            if ((int)GetHorIn() != 0)  WALK.SetActive(true);
        },
        (gObj) => { }));


        WALK = StateMgr.Add(new EntityState(
        (gObj) => { a.SetBool("walk", true); },
        (gObj) => {
            int axis_x = -(int)GetHorIn();
            if (axis_x != 0)
            {
                move_v = (axis_x * move_v) < 0 ? 0 : move_v;
                move_v += axis_x * stop_a * Time.deltaTime;
                if (math.abs(move_v) > 7f)
                {
                    move_v = axis_x * 7f;
                }
                d_r = axis_x < 0;
                a.SetBool("walk", false);
            }
            else
            {
                if (math.abs(move_v) <= stop_a * Time.deltaTime)
                {
                    move_v = 0f;
                }
                else
                {
                    move_v -= stop_a * Time.deltaTime * (move_v > 0 ? 1 : -1);
                }
            }
            setMovement(move_v);
            if (move_v == 0f)
            {
                WALK.SetActive(false);
            }
        },
        (gObj) => { a.SetBool("walk", false); }));

        //todo future
        //JUMP = StateMgr.Add(new EntityState());
        //OnAir = StateMgr.Add(new EntityState());
    }

    // Update is called once per frame
    void Update()
    {
        StateMgr.Iter((es) => {
            if(es.isActive()) es.OnUpdate(this.gameObject); 
        });

        Vector3 vec = visual.transform.localScale;
        if(d_r)
        {
            vec.x += Time.deltaTime * scale_x * 18;
            if(vec.x > scale_x)
                vec.x = scale_x;
        }
        else
        {
            vec.x -= Time.deltaTime * scale_x * 18;
            if (vec.x < -scale_x)
                vec.x = -scale_x;
        }
        visual.transform.localScale = vec;
    }

    void FixedUpdate()
    {
        ground_tick -= Time.deltaTime;
        if (ground_tick <= 0) ground_tick = -0.1f;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != _Collider) 
        { 
            onGround = true;
            a.SetBool("jump", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != _Collider)
        {
            onGround = false;
            ground_tick = 0.12f;
        }
    }


    private void doJump(float v)
    {
        Vector2 vec = _Rigidbody2D.velocity;
        vec.y = v;
        _Rigidbody2D.velocity = vec;
        a.SetBool("jump", true);
    }

    private void setMovement(float v)
    {
        Vector2 vec = _Rigidbody2D.velocity;
        vec.x = -v;
        _Rigidbody2D.velocity = vec;
    }

    public static float GetHorIn()
    {
        return Input.GetAxisRaw("Horizontal");
    }
}
