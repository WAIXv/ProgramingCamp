using Assets;
using Spine.Unity;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Spine;
using static S;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class irene_ctrl : MonoBehaviour
{
    #region SpineAnimation
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;
    [SpineBone(dataField: "skeletonAnimation")]
    public string boneName;
    Bone bone;

    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string idle_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string walk_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string jump_up_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string jump_up_2_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string jump_fall_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string skill_atk_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string skill_start_Anim;

    private string rootLayerAnim;
    #endregion

    #region Editable var
    [Header("Ctrl")]
    public Rigidbody2D rb;
    public GameObject visual;

    public float move_v_max = 0.7f;
    [SerializeField]
    private float stop_a = 32.0f;
    [SerializeField]
    private float jump_buffer_time = 0.4f;
    [SerializeField]
    private float grav_mul = 1.0f;
    [SerializeField]
    private player_Instance PInstance;
    [SerializeField]
    public float max_stun_tick { get; } = 0.35f;
    #endregion

    #region Audio
    [Header("Audio")]
    [SerializeField]
    private AudioSource a_Foot_step;
    [SerializeField]
    private AudioSource a_Landing;
    [SerializeField]
    private AudioSource a_Attack;

    #endregion

    #region Range
    [Header("Attack range")]
    public GameObject attack_range_mgr;
    [SerializeField]
    private GameObject basic_attack_range;
    [SerializeField]
    private GameObject ground_sensor;

    #endregion

    #region Walk buffer
    [Header("Buffer")]
    public bool face_r = true;
    public float move_v = 0f;
    Vector3 visual_scale;

    public int walk_state = 0;
    //public float last_pos_x = 0f;
    public float stun_tick = 0f;
    private float bladeTrail_stun = 0f;
    #endregion

    #region Jump buffer
    private float jump_buffer = 0;
    public int jump_state = 4;
    private float jump_press_time = 0;

    private float[] grav = { 8.8f, 18.9f, 1.2f, 21.1f, 5.8f };
    
    float ground_tick = 0.2f;
    private bool onGround = false;
    [SerializeField]
    private float jump_v = 10f;
    private bool lf_onGround = false;
    private bool isJumping = false;
    private float jumping_timer = 0f;
    private Vector3 lf_pos;
    #endregion

    #region Skill buffer
    [Header("Skill")]
    [SerializeField]
    private Texture2D t_SkillIcon;
    private bool onSkill = false;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        visual = gameObject.transform.Find("visual").gameObject;
        visual_scale = visual.transform.localScale;
        lf_pos = gameObject.transform.position;
        PInstance.Skill = new CustomRestoreSkill("判决" ,t_SkillIcon, 30);
        #region Initializating SpineAnimation
        bone = skeletonAnimation.Skeleton.FindBone(boneName);
        animationState = skeletonAnimation.AnimationState;
        animationState.SetAnimation(0, idle_Anim, true);
        rootLayerAnim = idle_Anim;
        animationState.Event += AnimEventHandler;
        #endregion
    }

    private void AnimEventHandler(TrackEntry trackEntry, Spine.Event e)
    {
        switch (e.Data.Name)
        {
            case "OnAttack_1":
                StartCoroutine(BA(0f));
                break;
            case "OnAttack_2":
                StartCoroutine(BA(13.5f));
                break;
            case "playWalkSound":
                a_Foot_step.Play();
                break;
            case "SkillAtk_1":
                a_Attack.Play();
                break;
        }
    }

    IEnumerator BA(float kb)
    {
        bladeTrail_stun = 0.2f;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Basic_Attack(kb);
    }

    private void Basic_Attack(float KnockBack)
    {
        GameObject[] tmp2 = Basic_Attack_Range();
        foreach (GameObject mob in tmp2)
        {
            entity_Instance MI = mob.GetComponent<entity_Instance>();
            if (MI != null)
            {
                a_Attack.Play();
                PInstance.Skill.AddSkillPoint(1f);
                float db = MI.PhysicsDamage(PInstance.ATK);
                MI.KnockBack(Vector2.right * (face_r ? 1 : -1) * KnockBack);
            }
        }
    }

    private GameObject[] Basic_Attack_Range()
    {
        return MyUtils.BoxRange(basic_attack_range.transform.position, basic_attack_range.transform.localScale / 2, 0f, Vector2.right * (face_r ? 1 : -1), 0f, LayerMask.GetMask("Mob"),"mob_obj");
    }


    // Update is called once per frame
    void Update()
    {
        onGround = MyUtils.GroundCheckBox(ground_sensor, 0.2f, LayerMask.GetMask("World", "Mob"));

        Vector2 speed = rb.velocity;
        float dt = Time.deltaTime;
        float dy = gameObject.transform.position.y - lf_pos.y;

        if (onGround)
        {
            if (!isJumping)
            {
                if (rootLayerAnim == walk_Anim)
                    setAnim(0, rootLayerAnim, true, math.abs(rb.velocity.x) * 0.5f);
                else
                    setAnim(0, rootLayerAnim, true);
                ground_tick = 0.1f;
                jump_state = 4;
            }
            if (!lf_onGround)
            {
                isJumping = false;
                if (dy < 0f) a_Landing.Play();
            }
            else
            {
                if (isJumping)
                {
                    jumping_timer += dt;
                    if (jumping_timer >= 0.2f)
                    {
                        jumping_timer = 0f;
                        isJumping = false;
                    }
                }
                else
                {
                    jumping_timer = 0f;
                }
            }
        }


        #region Face on
        //visual handle
        Vector3 vec = visual.transform.localScale;
        int _0x00 = (face_r ? 1 : -1);
        vec.x += dt * _0x00 * visual_scale.x * 18;
        if (math.abs(vec.x) > visual_scale.x) vec.x = visual_scale.x * _0x00;
        visual.transform.localScale = vec;
        //attack range handle
        vec = attack_range_mgr.transform.localScale;
        vec.x = math.abs(vec.x) * _0x00;
        attack_range_mgr.transform.localScale = vec;
        //Blade trail handle
        #endregion

        #region Vertical movment handle

        if ((int)Input.GetAxisRaw("Vertical") > 0 || Input.GetKey(KeyCode.Space))
        {
            if (jump_press_time <= 0f) jump_buffer = jump_buffer_time;
            jump_press_time += dt;
        }
        else
        {
            jump_press_time = 0f;
        }

        if ((onGround || ground_tick > 0) && jump_buffer > 0 && !isJumping)
        {
            isJumping = true;
            ground_tick = 0f;
            jump_buffer = 0f;
            speed.y = jump_v;
            jump_state = 1;
            animationState.SetAnimation(0, jump_up_Anim, false).TimeScale = 1f;
            animationState.AddAnimation(0, jump_up_2_Anim, true, 0f);
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
                if (speed.y <= -25f) jump_state++;
                break;
            case 5:
                speed.y -= grav[4] * grav_mul * dt;
                if (speed.y <= -29f) jump_state++;
                break;
            case 6: //匀速下坠
                speed.y = -29f;
                break;
            default:

                break;
        }
        #endregion


        #region Horizontal movment handle
        int axis_x = -(int)GetHorIn();
        if (axis_x != 0 && stun_tick <= 0)
        {
            face_r = axis_x < 0;
            walk_state = 1;
        }
        else
        {
            if (walk_state == 1 && stun_tick <= 0)
                walk_state = 2;
        }

        switch (walk_state)
        {
            case 0:
                move_v = 0;
                Vector3 v = gameObject.transform.position;
                v.x = lf_pos.x;
                gameObject.transform.position = v;
                break;
            case 1:
                move_v = (axis_x * move_v) < 0 ? 0 : move_v;
                move_v += axis_x * stop_a * Time.deltaTime;
                if (math.abs(move_v) > move_v_max * (onGround ? 1f : 1.5f))
                {
                    move_v = axis_x * move_v_max * (onGround ? 1f : 1.5f);
                }
                rootLayerAnim = walk_Anim;
                break;
            case 2:
                if (math.abs(move_v) <= stop_a * dt)
                {
                    walk_state = 0;
                    move_v = 0f;
                    rootLayerAnim = idle_Anim;
                }
                else
                {
                    move_v -= stop_a * (onGround ? 1 : 0.7f) * dt * (move_v > 0 ? 1 : -1);
                }
                break;
            case 3:
                rootLayerAnim = idle_Anim;
                move_v += (move_v>0 ? -1 : 1) * stop_a * dt;
                if (math.abs(move_v) <= stop_a * dt || stun_tick <= 0)
                {
                    walk_state = 0;
                    move_v = 0f;
                }
                break;
        }

        #endregion

        #region Attack handle
        if (Input.GetMouseButtonDown(0) && !onSkill)
        {
            TrackEntry track = animationState.GetCurrent(1);
            if (track == null || (track != null && track.Animation.Name == "<empty>"))
            { 
                setAnim(1, "Attack", false, 1.7f);
                if (onGround) StartCoroutine(basic_attack_movement());
                animationState.AddEmptyAnimation(1, 0.2f, 0f);
            }
        }
        #endregion

        #region Skill handle
        if (Input.GetKeyDown(KeyCode.R) && PInstance.Skill.isAvailable() && !onSkill)
        {
            StartCoroutine(StartSkill());
        }

        #endregion



        #region EndUpdate
        //handle animation
        if (rb.velocity.y < -0.01f && !onGround)
        {
            setAnim(0, jump_fall_Anim, true);
        }

        if (ground_tick >= 0f) ground_tick -= dt;
        if (jump_buffer > 0f) jump_buffer -= dt;
        if (stun_tick > 0f) stun_tick -= dt;
        if (bladeTrail_stun >= 0f) bladeTrail_stun -= dt;

        speed.x = -move_v;

        rb.velocity = speed;
        lf_pos = gameObject.transform.position;
        lf_onGround = onGround;
        #endregion

    }
    #region Utils
    private IEnumerator StartSkill()
    {
        onSkill = true;
        setAnim(1, skill_start_Anim, false, 1.7f);
        animationState.AddAnimation(1, skill_atk_Anim, false, 0f);
        animationState.AddEmptyAnimation(1, 0.2f, 0f);
        PInstance.Skill.ClearSkillPoint();
        yield return new WaitForSpineAnimationEnd(animationState.GetCurrent(1));
        yield return new WaitForSpineAnimationEnd(animationState.GetCurrent(1));
        onSkill = false;
    }


    private IEnumerator basic_attack_movement()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        walk_state = 3;
        move_v = (face_r ? -1 : 1) * 3.8f;
        stun_tick = 0.35f;
        yield return new WaitForSeconds(0.12f);
        walk_state = 0;
    }

    public void setMove_v(float a)
    {
        move_v = a;
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
    #endregion
}
