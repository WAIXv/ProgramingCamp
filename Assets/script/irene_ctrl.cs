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
    #endregion

    [Header("Ctrl")]
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject visual;
    Vector3 visual_scale;
    public bool face_r = true;
    private bool onGround = false;

    public float move_v = 0f;

    [SerializeField]
    public float move_v_max = 0.7f;
    [SerializeField]
    private static float stop_a = 32.0f;
    [SerializeField]
    public float jump_v = 10.0f;

    float ground_tick = 0.2f;


    private string rootLayerAnim;


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
            if (math.abs(move_v) > move_v_max)
            {
                move_v = axis_x * move_v_max;
            }
            face_r = axis_x < 0;
            //animator.SetBool("walk", true);
            rootLayerAnim = walk_Anim;
        }
        else
        {
            if (math.abs(move_v) <= stop_a * Time.deltaTime)
            {
                move_v = 0f;
                rootLayerAnim = idle_Anim;
            }
            else
            {
                move_v -= stop_a * (onGround ? 1 : 0.7f) * Time.deltaTime * (move_v > 0 ? 1 : -1);
            }
        }
        //animator.SetFloat("walk_speed", math.abs(rb.velocity.x) / move_v_max);
        setMovement(move_v);

        if (rb.velocity.y < -0.01f && !onGround)
        {
            setAnim(0, jump_fall_Anim, true);
        }

        //else setAnim(0, rootLayerAnim, true);

        if (ground_tick >= 0f) ground_tick -= dt;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(!collision.isTrigger)
        {
            onGround = true;
            if (rootLayerAnim == walk_Anim)
                setAnim(0, rootLayerAnim, true, math.abs(rb.velocity.x) * 0.45f);
            else
                setAnim(0, rootLayerAnim, true);
            ground_tick = 0.2f;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            onGround = false;
        }
    }


    private void doJump(float v)
    {
        Vector2 vec = rb.velocity;
        vec.y = v;
        rb.velocity = vec;
        setAnim(0, jump_up_Anim, false);
        addAnim(0, jump_up_2_Anim, true, 0f);
    }

    private void setMovement(float v)
    {
        Vector2 vec = rb.velocity;
        vec.x = -v;
        rb.velocity = vec;
    }
    private void updateFall(float v)
    {
        Vector2 vec = rb.velocity;
        vec.y = -v;
        rb.velocity = vec;
    }
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

}
