using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class dslntc_AI : MonoBehaviour
{
    [SerializeField] 
    mob_Instance mInstance;
    // Start is called before the first frame update
    [SerializeField]
    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;

    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    GameObject visual;
    [SerializeField]
    GameObject attack_range_root;
    [SerializeField]
    GameObject attack_range;

    private GameObject attack_target;

    private bool face_r = true;
    private int skill = 0;

    // Start is called before the first frame update
    void Start()
    {
        animationState = skeletonAnimation.AnimationState;
        mInstance.beforeDeath = () => {
            animationState.SetAnimation(0, "Die", false);
            skill = -1;
        };
        mInstance.onDamage = () => {
            SkillStart();
        };
        mInstance.wait_for_death = 1f;
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
                    player_Instance PIns = tmp2[0].GetComponent<player_Instance>();
                    if(PIns != null)
                    {
                        PIns.Damage(0f);
                        PIns.Obj.GetComponent<irene_ctrl>().setMove_v((face_r ? -1 : 1) * 20f);
                    }
                }

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
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

        if (skill == 1)
        {
            GameObject[] targets = Attack_Range();
            if (targets.Length > 0) Attack();

            mInstance.health -= mInstance.getMaxHealth() * 0.05f * Time.deltaTime;
        }
    }

    private void Basic_Attack(float KnockBack)
    {
        GameObject[] tmp2 = Attack_Range();
        foreach (GameObject mob in tmp2)
        {
            mob_Instance MI = mob.GetComponent<mob_Instance>();
            if (MI != null)
            {
                float db = MI.Damage(500);
                MI.rigidbody.velocity += Vector2.right * (face_r ? 1 : -1) * KnockBack;
                Debug.Log("Damage: " + db + "\n MOB_HEALTH: " + (int)MI.health + "/" + (int)MI.getMaxHealth());
            }
        }
    }





    private void SkillStart()
    {
        if (skill == 0)
        {
            skill = 1;
            animationState.SetAnimation(0, "Skill_Begin", false);
            animationState.AddAnimation(0, "Skill_Idle", true, 0f);
        }
    }

    private GameObject[] Attack_Range()
    {
        Vector2 Button = attack_range.transform.position;
        Vector2 Scale = attack_range.transform.localScale;
        RaycastHit2D[] hits = Physics2D.BoxCastAll(Button, Scale / 2, 0, Vector2.right * (face_r ? 1 : -1), 0f);
        List<GameObject> result = new List<GameObject>();
        foreach (RaycastHit2D hit in hits)
        {
            if (!hit.collider.isTrigger && hit.collider.gameObject.tag == "player_obj" && result.IndexOf(hit.collider.gameObject) < 0)
            {
                result.Add(hit.collider.gameObject);
            }
        }
        return result.ToArray();
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
