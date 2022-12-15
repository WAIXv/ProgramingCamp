using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator enemyAnim;
    protected Collider2D enemyColl;
    protected Rigidbody2D enemyRb;
    protected bool isDeathing=false;
    protected PlayerCtrler playerCtrler;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.position.y > transform.position.y +1&&other.rigidbody.velocity.y<=0)
            {
                playerCtrler = other.gameObject.GetComponent<PlayerCtrler>();
                playerCtrler.GiveDashChance();
                other.rigidbody.velocity = new Vector2(other.rigidbody.velocity.x, 12);
                AudioManager.instance.PlayDeathSound();
                isDeathing = true;
                enemyAnim.Play("Death");
            }
        }
    }
}
