using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCtrler : Enemy
{
    #region 声明部分
    [Header("面向方向")]
    [SerializeField] private Vector3 scaleLeft;
    [SerializeField] private Vector3 scaleRight;
    private bool faceleft = true;                             //是否面向左侧

    [Header("移动相关")]
    [SerializeField] private float left;                      //左右距离限制
    [SerializeField] private float right;
    [SerializeField] private Vector2 speedLeft;
    [SerializeField] private Vector2 speedRight;

    [Header("组件")]
    [SerializeField] private Rigidbody2D rigidbodyFrog;
    [SerializeField] private Collider2D colliderFrog;
    [SerializeField] private LayerMask ground;
    [SerializeField] private PlayerCtrler playerCtrler;
    #endregion

    protected override void Start()
    {
        base.Start();
        colliderFrog=GetComponent<Collider2D>();
        rigidbodyFrog=GetComponent<Rigidbody2D>();
        left=transform.position.x-3;
        right=transform.position.x+3;
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
                    transform.localScale = scaleLeft;
                }
                else if (colliderFrog.IsTouchingLayers(ground))
                {
                    rigidbodyFrog.velocity = speedLeft;
                }
            }
            else
            {
                if (transform.position.x > right)
                {
                    faceleft = true;
                    transform.localScale = scaleRight;
                }
                else if (colliderFrog.IsTouchingLayers(ground))
                {
                    rigidbodyFrog.velocity = speedRight;
                }
            }
        }
    }
    private void SwichAnimFrog()
    {
        if(rigidbodyFrog.velocity.y>0.5)enemyAnim.SetBool("frogJumping",true);
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
    }//在死亡动画结束后销毁实体
    /// <summary>
    /// BeforeDeath方法用来在死亡动画播放前先将其刚体与碰撞体销毁
    /// </summary>
    private void BeforeDeath()
    {
        Destroy(rigidbodyFrog);
        Destroy(colliderFrog);
    }
}
