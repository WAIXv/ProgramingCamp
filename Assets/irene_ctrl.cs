using Assets;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;
using static S;

public class irene_ctrl : MonoBehaviour
{
    public static class IRENE
    {
        /*
        public class IDLE : EntityStates.IState
        {
            private irene_ctrl Obj;
            public IDLE(irene_ctrl obj)
            {
                Obj = obj;
            }

            public void OnEnter() { }

            public void OnLeave() { }

            public void OnUpdate()
            {

            }
        }

        public class MOVE : EntityStates.IState
        {
            private irene_ctrl Obj;
            public MOVE(irene_ctrl obj)
            {
                Obj = obj;
            }

            public void OnEnter() { }

            public void OnLeave() { }

            public void OnUpdate()
            {

            }

        }
        */

    }

    //public EntityStates.IState EState;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject visual;
    Vector3 visual_scale;
    public bool face_r = true;
    private bool onGround = false;
    private bool jump_giving_velocity = false;


    public float move_v = 0f;

    [SerializeField]
    public float move_v_max = 0.7f;
    [SerializeField]
    private static float stop_a = 32.0f;
    [SerializeField]
    public float jump_v = 10.0f;

    float ground_tick = 0.2f;


    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        //EState = new IRENE.IDLE(this);

        rb = GetComponent<Rigidbody2D>();

        visual = gameObject.transform.Find("visual").gameObject;
        animator = visual.GetComponent<Animator>();
        visual_scale = visual.transform.localScale;


    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        //face on
        Vector3 vec = visual.transform.localScale;
        int _0x00 = (face_r ? 1 : -1);

        //vec.x = visual_scale.x * _0x00;
        vec.x += dt * _0x00 * visual_scale.x * 18;
        if (math.abs(vec.x) > visual_scale.x) vec.x = visual_scale.x * _0x00;
        visual.transform.localScale = vec;

        //input handle
        if ((onGround || ground_tick > 0) && (Input.GetKeyDown(KeyCode.Space) || (int)Input.GetAxisRaw("Vertical") > 0))
        {
            doJump(jump_v);
            onGround = false;
            ground_tick = -0.1f;
        }

        int axis_x = -(int)GetHorIn();
        if (axis_x != 0)
        {
            move_v = (axis_x * move_v) < 0 ? 0 : move_v;
            move_v += axis_x * stop_a * Time.deltaTime;
            if (math.abs(move_v) > 7f)
            {
                move_v = axis_x * 7f;
            }
            face_r = axis_x < 0;
            animator.SetBool("walk", true);
        }
        else
        {
            if (math.abs(move_v) <= stop_a * Time.deltaTime)
            {
                move_v = 0f;
                animator.SetBool("walk", false);
            }
            else
            {
                move_v -= stop_a * Time.deltaTime * (move_v > 0 ? 1 : -1);
            }
        }
        animator.SetFloat("walk_speed", math.abs(move_v) / move_v_max);
        setMovement(move_v);

        if (rb.velocity.y < -0.05f && !onGround) animator.SetBool("fall", true);
        else animator.SetBool("fall", false);

        if (ground_tick >= 0f) ground_tick -= dt;
    }

    

    void OnTriggerStay2D(Collider2D collision)
    {
        if(!collision.isTrigger)
        {
            onGround = true;
            animator.SetBool("jump", false);
            ground_tick = 0.2f;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            onGround = false;
            jump_giving_velocity = false;
        }
    }


    private void doJump(float v)
    {
        Vector2 vec = rb.velocity;
        vec.y = v;
        rb.velocity = vec;
        animator.SetBool("jump", true);
    }

    private void setMovement(float v)
    {
        Vector2 vec = rb.velocity;
        vec.x = -v;
        rb.velocity = vec;
    }

    public static float GetHorIn()
    {
        return Input.GetAxisRaw("Horizontal");
    }
}
