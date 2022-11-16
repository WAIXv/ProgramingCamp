using Assets;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;
using Spine;
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


    #region SpineAnimation
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;
    [SpineBone(dataField: "skeletonAnimation")]
    public string boneName;
    Bone bone;

    [SerializeField] private string idle_Anim;
    [SerializeField] private string walk_Anim;
    [SerializeField] private string jump_up_Anim;
    [SerializeField] private string jump_up_2_Anim;
    [SerializeField] private string jump_fall_Anim;

    private string rootLayerAnim;
    #endregion

    #region Editable var
    [Header("Ctrl")]
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject visual;

    
    public float move_v_max = 0.7f;
    [SerializeField]
    private float stop_a = 32.0f;
    [SerializeField]
    private float jump_buffer_time = 0.4f;
    [SerializeField]
    private float grav_mul = 1.0f;
    #endregion

    #region Walk buffer
    public bool face_r = true;
    private float move_v = 0f;
    Vector3 visual_scale;

    public int walk_state = 0;
    public float last_pos_x = 0f;

    #endregion

    #region Jump buffer
    private float jump_buffer = 0;
    private int jump_state = 4;
    private float jump_press_time = 0;

    private float[] grav = { 8.8f, 18.9f, 1.2f, 21.1f, 5.8f };
    
    float ground_tick = 0.2f;
    private bool onGround = false;
    [SerializeField]
    private float jump_v = 10f;

    #endregion




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        visual = gameObject.transform.Find("visual").gameObject;
        animator = visual.GetComponent<Animator>();
        visual_scale = visual.transform.localScale;
        last_pos_x = gameObject.transform.position.x;
        #region Initializating SpineAnimation
        bone = skeletonAnimation.Skeleton.FindBone(boneName);
        animationState = skeletonAnimation.AnimationState;
        animationState.SetAnimation(0, idle_Anim, true);
        rootLayerAnim = idle_Anim;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speed = rb.velocity;
        float dt = Time.deltaTime;

        #region Face on
        Vector3 vec = visual.transform.localScale;
        int _0x00 = (face_r ? 1 : -1);
        vec.x += dt * _0x00 * visual_scale.x * 18;
        if (math.abs(vec.x) > visual_scale.x) vec.x = visual_scale.x * _0x00;
        visual.transform.localScale = vec;
        #endregion

        #region Vertical movment handle

        if ((int)Input.GetAxisRaw("Vertical") > 0 || Input.GetKey(KeyCode.Space))
        {
            jump_buffer = jump_buffer_time;
            jump_press_time += dt;
        }
        else
        {
            jump_press_time = 0f;
        }


        if ((onGround || ground_tick > 0) && jump_buffer > 0)
        {
            speed.y = jump_v;
            if(jump_state >= 4)
            {
                jump_state = 1;
                setAnim(0, jump_up_Anim, false);
                addAnim(0, jump_up_2_Anim, true, 0f);
            }
        }

        switch (jump_state)
        {
            case 1: //减速上升
                speed.y -= grav[0] * grav_mul * dt;
                if (jump_press_time >= 0.4f || jump_press_time == 0f) jump_state++;
                break;
            case 2: //减速 1 挡
                speed.y -= grav[1] * grav_mul * dt;
                if (speed.y <= 0.1f) jump_state++;
                break;
            case 3: //减速 2 挡（近似滞留）
                speed.y -= grav[2] * grav_mul * dt;
                if (speed.y <= -0.08f) jump_state++;
                break;
            case 4: //加速下坠
                speed.y -= grav[3] * grav_mul * dt;
                if (speed.y <= -19f) jump_state++;
                break;
            case 5: //缓慢加速下坠
                speed.y -= grav[4] * grav_mul * dt;
                if (speed.y <= -26f) jump_state++;
                break;
            case 6: //匀速下坠
                speed.y = -26f;
                break;
            default: 
                
                break;
        }
        #endregion

        #region Horizontal movment handle
        int axis_x = -(int)GetHorIn();
        if (axis_x != 0)
        {
            face_r = axis_x < 0;
            walk_state = 1;
        }
        else
        {
            if (walk_state == 1)
                walk_state = 2;
        }

        switch (walk_state)
        {
            case 0:
                move_v = 0;
                Vector3 v = gameObject.transform.position;
                v.x = last_pos_x;
                gameObject.transform.position = v;
                break;
            case 1:
                move_v = (axis_x * move_v) < 0 ? 0 : move_v;
                move_v += axis_x * stop_a * Time.deltaTime;
                if (math.abs(move_v) > move_v_max)
                {
                    move_v = axis_x * move_v_max;
                }
                rootLayerAnim = walk_Anim;
                break;
            case 2:
                if (math.abs(move_v) <= stop_a * Time.deltaTime)
                {
                    walk_state = 0;
                    move_v = 0f;
                    rootLayerAnim = idle_Anim;
                }
                else
                {
                    move_v -= stop_a * (onGround ? 1 : 0.7f) * Time.deltaTime * (move_v > 0 ? 1 : -1);
                }
                break;
        }
        speed.x = -move_v;

        #endregion

        #region EndUpdate
        //handle animation
        if (rb.velocity.y < -0.01f && !onGround)
        {
            setAnim(0, jump_fall_Anim, true);
        }

        if (ground_tick >= 0f) ground_tick -= dt;
        if (jump_buffer > 0f) jump_buffer -= dt;

        rb.velocity = speed;

        last_pos_x = gameObject.transform.position.x;
        #endregion
    }

    private void LateUpdate()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(!collision.isTrigger)
        {
            onGround = true;
            if (rootLayerAnim == walk_Anim)
                setAnim(0, rootLayerAnim, true, math.abs(rb.velocity.x) * 0.482f);
            else
                setAnim(0, rootLayerAnim, true);
            ground_tick = 0.2f;
            jump_state = 4;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            onGround = false;
        }
    }

    #region Utils
    public static float GetHorIn()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public TrackEntry setAnim(int layer, string anim, bool loop, float timeScale)
    {
        TrackEntry track = animationState.GetCurrent(layer);
        if (track != null)
        {
            if(track.Animation.Name != anim) track = animationState.SetAnimation(layer, anim, loop);
        }
        else
        {
            track = animationState.SetAnimation(layer, anim, loop);
        }
        if (track.TimeScale != timeScale) track.TimeScale = timeScale;
        return track;
    }

    public TrackEntry setAnim(int layer, string anim, bool loop)
    {
        return setAnim(layer, anim, loop, 1.0f);
    }

    public TrackEntry addAnim(int layer, string anim, bool loop, float delay)
    {
        if (animationState.GetCurrent(layer) != null)
        {
            if (animationState.GetCurrent(layer).Animation.Name != anim)
                return animationState.AddAnimation(layer, anim, loop, delay);
        }
        else
        {
            return animationState.AddAnimation(layer, anim, loop, delay);
        }
        return animationState.GetCurrent(layer);
    }
    #endregion
}
