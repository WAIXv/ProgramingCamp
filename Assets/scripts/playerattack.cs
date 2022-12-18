using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class playerattack : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;
    private Animator anim;
    private PolygonCollider2D coll2D;
    public AudioSource attackMusic;
    void Start()
    {
        
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        coll2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    void Attack()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if(info.normalizedTime >= 0.817f)
        {
            anim.SetBool("isattacking", false);
        }
        if (Input.GetButtonDown("attack")&&anim.GetBool("isattacking")==false)
        {
            anim.SetTrigger("attack");
            attackMusic.Play();
            StartCoroutine(StartAttack());
            anim.SetBool("isattacking", true);
        }
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);//—” ±
        coll2D.enabled = true;
        StartCoroutine(disabledHitbox());
    }
    IEnumerator disabledHitbox()
    {
        yield return new WaitForSeconds(time);//—” ±
        coll2D.enabled = false;
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemies")|| other.gameObject.CompareTag("boss"))
        {
            other.GetComponent<Enemy1>().TakeDamage(damage);
        }
    }
}
