using Assets;
using Spine.Unity;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Spine;

public class texes_ctrl : MonoBehaviour
{
    #region SpineAnimation
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;

    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string idle_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string walk_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string jump_up_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string jump_up_2_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string jump_fall_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string basic_atk_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string skill_atk_Anim;
    [SerializeField][SpineAnimation(dataField: "skeletonAnimation")] private string skill_start_Anim;

    private string rootLayerAnim;
    #endregion

    #region Particle
    [Header("Particle")]
    [SerializeField]
    private GameObject P_Root;
    [SerializeField]
    private ParticleSystem P_Left;
    [SerializeField]
    private ParticleSystem P_Right;
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

    #region Audio & Texture
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
    #endregion

    #region Skill buffer
    [Header("Skill")]
    [SerializeField]
    private Texture2D t_SkillIcon;
    [SerializeField]
    private float Skill_time = 8f;
    private float Skill_timer = 0f;
    private bool skill_first = false;
    #endregion


    private Vector3 lf_pos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        visual = gameObject.transform.Find("visual").gameObject;
        visual_scale = visual.transform.localScale;
        lf_pos = gameObject.transform.position;
        PInstance.Skill = new TexasSkill("阵雨连绵", t_SkillIcon, 10f);
    }

    void Start()
    {
        #region Initializating SpineAnimation
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
                if (isOnSkill())
                {
                    if (skill_first)
                    {
                        StartCoroutine(BA(PInstance.ATK * 4f, 6f));
                        P_Left.Play();
                        P_Right.Play();
                        skill_first = false;
                    }
                    else
                    {
                        StartCoroutine(BA(PInstance.ATK * 1.8f, 5f));
                        P_Left.Play();
                    }
                }
                else
                {
                    StartCoroutine(BA(PInstance.ATK, 5f));
                }
                    
                    
                break;
            case "OnAttack_2":
                StartCoroutine(BA(PInstance.ATK * 1.8f, 5f));
                P_Right.Play();
                break;
            case "playWalkSound":
                a_Foot_step.Play();
                break;
        }
    }

    private bool isOnSkill()
    {
        return Skill_timer > 0f;
    }



    IEnumerator BA(float d,float kb)
    {
        bladeTrail_stun = 0.15f;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Basic_Attack(d,kb);
    }

    private void Basic_Attack(float d,float KnockBack)
    {
        GameObject[] tmp2 = Basic_Attack_Range();
        foreach (GameObject mob in tmp2)
        {
            entity_Instance MI = mob.GetComponent<entity_Instance>();
            if (MI != null)
            {
                a_Attack.Play();
                if(isOnSkill()) MI.MagicDamage(d);
                else MI.PhysicsDamage(d);
                MI.KnockBack(Vector2.right * (face_r ? 1 : -1) * KnockBack);
                if (MI.isDeath()) PInstance.Skill.AddSkillPoint(1f);
            }
        }
    }

    private GameObject[] Basic_Attack_Range()
    {
        return MyUtils.BoxRange(basic_attack_range.transform.position, basic_attack_range.transform.localScale / 2, 0f, Vector2.right * (face_r ? 1 : -1), 0f, LayerMask.GetMask("Mob"), "mob_obj");
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
                    setAnim(0, rootLayerAnim, true, math.abs(rb.velocity.x) * 0.58f);
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
        }

        #region Face on
        //visual handle
        Vector3 vec = visual.transform.localScale;
        int _0x00 = (face_r ? 1 : -1);
        vec.x += dt * _0x00 * visual_scale.x * 18;
        if (math.abs(vec.x) > visual_scale.x) vec.x = visual_scale.x * _0x00;
        visual.transform.localScale = vec;
        //attack range handle
        //Blade trail handle
        if (bladeTrail_stun <= 0)
        {
            vec = attack_range_mgr.transform.localScale;
            vec.x = math.abs(vec.x) * _0x00;
            attack_range_mgr.transform.localScale = vec;
            vec = P_Root.transform.localScale;
            vec.y = math.abs(vec.y) * _0x00;
            vec.x = math.abs(vec.x) * _0x00;
            P_Root.transform.localScale = vec;
            vec = P_Root.transform.rotation.eulerAngles;
            vec.y = face_r ? 0 : 180f;
            P_Root.transform.rotation = Quaternion.Euler(vec);
        }
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
                move_v += (move_v > 0 ? -1 : 1) * stop_a * dt;
                if (math.abs(move_v) <= stop_a * dt || stun_tick <= 0)
                {
                    walk_state = 0;
                    move_v = 0f;
                }
                break;
        }

        #endregion

        #region Attack handle
        if (Input.GetMouseButtonDown(0))
        {
            TrackEntry track = animationState.GetCurrent(1);
            if (track == null || (track != null && track.Animation.Name == "<empty>"))
            {
                if(isOnSkill()) setAnim(1, skill_atk_Anim, false, 1.7f);
                else setAnim(1, basic_atk_Anim, false, 1.7f);
                if (onGround) StartCoroutine(basic_attack_movement());
                animationState.AddEmptyAnimation(1, 0.2f, 0f);
            }
        }
        #endregion

        #region Skill handle
        if (Input.GetKeyDown(KeyCode.R) && !isOnSkill() && PInstance.Skill.isAvailable())
        {
            skill_first = true;
            if (onGround) setAnim(1, skill_start_Anim, false, 1.7f);
            else setAnim(1, skill_atk_Anim, false, 1.7f);
            
            if (onGround) StartCoroutine(basic_attack_movement());
            animationState.AddEmptyAnimation(1, 0.2f, 0f);

            Skill_timer = Skill_time;
            PInstance.Skill.ClearSkillPoint();
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
        if (Skill_timer >= 0f) Skill_timer -= dt;

        PInstance.Skill.SkillTimeRate = Skill_timer / Skill_time;

        speed.x = -move_v;

        rb.velocity = speed;
        lf_pos = gameObject.transform.position;
        lf_onGround = onGround;
        #endregion

    }
    #region Utils
    private IEnumerator basic_attack_movement()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        walk_state = 3;
        move_v = (face_r ? -1 : 1) * 3.5f;
        stun_tick = 0.28f;
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
            if (track.Animation.Name != anim) track = animationState.SetAnimation(layer, anim, loop);
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
