using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerskill : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;
    public int MP;
    private Animator anim;
    private PolygonCollider2D coll2D;
    int cut = 0;
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();
       MPBar.MPmax = MP;
        MPBar.MPCurrent = MP;
    }

    // Update is called once per frame
    void Update()
    {
        cut++;
       Skill();
    }
    void Skill()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.7f)
        {
            anim.SetBool("isskilling", false);
        }
        if (Input.GetButtonDown("skill") && anim.GetBool("isskilling") == false&&MP>=3)
        {
            MP -= 3;
            MPBar.MPCurrent = MP;
            anim.SetTrigger("skill");
            StartCoroutine(StartSkilling());
            anim.SetBool("isskilling", true);
        }
    }
    public void RecoveyMP(int recovey)
    {
        
        if (MPBar.MPCurrent + recovey <= MPBar.MPmax)
        {
            MP += recovey;
            MPBar.MPCurrent += recovey;
        }
        else
        {
            MP = MPBar.MPmax;
            MPBar.MPCurrent = MPBar.MPmax;
        }
    }
    IEnumerator StartSkilling()
    {
        yield return new WaitForSeconds(startTime);//ÑÓÊ±
        coll2D.enabled = true;
        StartCoroutine(disabledHitbox());
    }
    IEnumerator disabledHitbox()
    {
        yield return new WaitForSeconds(time);//ÑÓÊ±
        coll2D.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemies"))
        {
            if (cut >= 400)
            {
                other.GetComponent<Enemy1>().TakeDamage(damage);
                cut = 0;
            }
        }
        if(other.gameObject.CompareTag("boss"))
        {
            if (cut >= 400)
            {
                other.GetComponent<Enemy1>().TakeDamage(damage);
                cut = 0;
            }
        }

    }
}
