using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack1 : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float time;
    [SerializeField] private Animator animator;
    [SerializeField] private PolygonCollider2D Collider2D;
    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Collider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HeroKnight_Attack1") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.05f) 
        {
            Collider2D.enabled = true;
            StartCoroutine(Disabled());
        }
    }

    IEnumerator Disabled()
    {
        yield return new WaitForSeconds(time);
        Collider2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().takeDamaeg(damage);
        }
    }
}
