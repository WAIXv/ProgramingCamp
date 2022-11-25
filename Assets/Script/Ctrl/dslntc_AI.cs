using Assets;
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class dslntc_AI : MonoBehaviour
{
    [SerializeField]
    entity_Instance EInstance;
    // Start is called before the first frame update
    [SerializeField]
    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;

    private GameObject target;

    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    GameObject visual;
    [SerializeField]
    private float move_v = 18f;
    [SerializeField]
    private float blood_lost_rate_ps = 5f;
    [Header("Attack_ranges")]
    [SerializeField]
    GameObject attack_range_root;
    [SerializeField]
    GameObject attack_range;
    [SerializeField]
    GameObject attack_lock;
    [SerializeField]
    GameObject skill_range;

    private GameObject attack_target;

    #region Movement buffer
    private int move_state;

    #endregion

    private bool face_r = true;
    private int skill = 0;

    // Start is called before the first frame update
    void Start()
    {
        animationState = skeletonAnimation.AnimationState;
        EInstance.beforeDeath = () => {
            skill = -1;
            animationState.SetAnimation(0, "Die", false);
        };
        EInstance.onDamage = () => {
            SkillStart();
        };
        EInstance.wait_for_death = 1f;
        animationState.Event += AnimEventHandler;
    }

    private void AnimEventHandler(TrackEntry trackEntry, Spine.Event e)
    {
        switch (e.Data.Name)
        {
            case "OnAttack":
                GameObject[] tmp2 = Attack_Range();
                if(tmp2.Length > 0)
                {
                    entity_Instance PIns = tmp2[0].GetComponent<entity_Instance>();
                    if(PIns != null)
                    {
                        PIns.PhysicsDamage(EInstance.ATK);
                        PIns.KnockBack(Vector2.right * (face_r ? -1 : 1) * 20f);
                        //PIns.Obj.GetComponent<irene_ctrl>().setMove_v((face_r ? -1 : 1) * 20f);
                    }
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EInstance.health <= 0) return;
        if(skill != 2)
        {
            Vector3 v = gameObject.transform.localScale;
            if (rb.velocity.x > 1f) face_r = true;
            else if (rb.velocity.x < -1f) face_r = false;
            v.x = math.abs(v.x) * (face_r ? 1 : -1);
            visual.transform.localScale = v;
        }

        if (math.abs(rb.velocity.x) >= 2f)
        {
            SkillStart();
        }

        if (skill >= 1)
        {
            if(target == null)
            {
                GameObject[] tmp = MyUtils.BoxRange(skill_range.transform.position, skill_range.transform.localScale / 2, 0, Vector2.right * (face_r ? 1 : -1), 0f, "player_obj");
                if (tmp.Length > 0)
                    target = tmp[0];
            }
            EInstance.health -= EInstance.getMaxHealth() * (blood_lost_rate_ps / 100f) * Time.deltaTime;
        }

        if (skill == 1)
        {
            GameObject[] targets = Attack_check();
            if (targets.Length > 0) Attack();

            if(target!= null)
            {
                Vector2 speed = rb.velocity;
                if (target.transform.position.x - gameObject.transform.position.x > 0.55f)
                    speed.x = move_v;
                else if (target.transform.position.x - gameObject.transform.position.x < -0.55f)
                    speed.x = -move_v;
                else
                    speed.x = 0;
                rb.velocity = speed;
            }
        }

    }

    private void SkillStart()
    {
        if (skill == 0)
        {
            skill = 2;
            animationState.SetAnimation(0, "Skill_Begin", false);
            animationState.AddAnimation(0, "Skill_Idle", true, 0f);
            StartCoroutine(delayChangeState());
        }
    }
    
    IEnumerator delayChangeState()
    {
        yield return new WaitForSpineAnimationEnd(animationState.GetCurrent(0));
        skill = 1;
    }

    private GameObject[] Attack_check()
    {
        return MyUtils.BoxRange(attack_lock.transform.position, attack_lock.transform.localScale / 2, 0, Vector2.right * (face_r ? 1 : -1), 0f, "player_obj");
    }


    private GameObject[] Attack_Range()
    {
        return MyUtils.BoxRange(attack_range.transform.position, attack_range.transform.localScale / 2, 0, Vector2.right * (face_r ? 1 : -1), 0f, "player_obj");
    }

    private void Attack()
    {
        if(skill == 1)
        {
            skill = 2;
            animationState.SetAnimation(0, "Skill_Attack", false);
            animationState.AddAnimation(0, "Skill_Idle", true ,0f);
            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSpineAnimationEnd(animationState.GetCurrent(0));
        skill = 1;
    }


}
