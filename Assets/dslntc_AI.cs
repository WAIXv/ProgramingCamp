using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class dslntc_AI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;


    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    GameObject visual;

    private int skill = 0;

    // Start is called before the first frame update
    void Start()
    {
        animationState = skeletonAnimation.AnimationState;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = gameObject.transform.localScale;
        if (rb.velocity.x > 0.005f)
            v.x = math.abs(v.x);
        else if (rb.velocity.x < -0.005f)
            v.x = -math.abs(v.x);
        visual.transform.localScale = v;


        if (math.abs(rb.velocity.x) >= 2f && skill == 0)
        {
            skill = 1;
            animationState.SetAnimation(0, "Skill_Begin", false);
            animationState.AddAnimation(0, "Skill_Idle", true, 0f);
        }

        if (skill == 1)
        {
            skill = 2;
            StartCoroutine(WaitForDeath());
        }
    }
    IEnumerator WaitForDeath()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            animationState.SetAnimation(0, "Die", false);
            yield return new WaitForSeconds(1f);
            GameObject.Destroy(gameObject);
        }
    }
}
