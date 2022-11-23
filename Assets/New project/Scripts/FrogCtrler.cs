using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCtrler : Enemy
{
    private bool faceleft = true;                                   //是否面向左侧
    [SerializeField] private Rigidbody2D rigidbodyFrog;
    [SerializeField] private Collider2D colliderFrog;
    [SerializeField] private float left,right;                      //左右距离限制
    [SerializeField] private float speed, jumpForce;
    [SerializeField] private LayerMask ground;
    [SerializeField] private PlayerCtrler playerCtrler;

    protected override void Start()
    {
        base.Start();
        colliderFrog=GetComponent<Collider2D>();
        rigidbodyFrog=GetComponent<Rigidbody2D>();
        left=transform.position.x-5;
        right=transform.position.x+5;
    }
    void Update()
    {
        if(!isDeathing)SwichAnimFrog();
    }
    private void FrogMove()
    {
        if (!isDeathing)
        {
            if (faceleft)
            {
                if (transform.position.x < left)
                {
                    faceleft = false;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (colliderFrog.IsTouchingLayers(ground))
                {
                    rigidbodyFrog.velocity = new Vector2(-speed, jumpForce);
                }
            }
            else
            {
                if (transform.position.x > right)
                {
                    faceleft = true;
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else if (colliderFrog.IsTouchingLayers(ground))
                {
                    rigidbodyFrog.velocity = new Vector2(speed, jumpForce);
                }
            }
        }
    }
    private void SwichAnimFrog()
    {
        if(rigidbodyFrog.velocity.y>0)enemyAnim.SetBool("frogJumping",true);
        if (colliderFrog.IsTouchingLayers(ground)) enemyAnim.SetBool("frogFalling", false);
        if (rigidbodyFrog.velocity.y < 0)
        {
            enemyAnim.SetBool("frogJumping", false);
            enemyAnim.SetBool("frogFalling", true);
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }
    private void BeforeDeath()
    {
        Destroy(rigidbodyFrog);
        Destroy(colliderFrog);
    }
}
