using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playcontrol : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;
    public float speed;
    public float jumpfore;
    public LayerMask ground;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()   
    {
        Movement();
        SwitchAnim();

    }

    void Movement()  //Movement 为自定义 函数
    {
        float Horizontalmove = Input.GetAxis("Horizontal");// Horizontal 只有三个参数 1向右 -1向左 0不动;
        float facedirection = Input.GetAxisRaw("Horizontal");//GetAxisRaw 和 GetAxis 不一样的是 它能直接获取 1 -1 0

        #region 角色移动
        if (Horizontalmove != 0)
        {
            //用刚体来改变速度 velocity速度的变化    //vector2 2D平台上 X Y 轴上移动的变化
            rb.velocity = new Vector2(Horizontalmove * speed , rb.velocity.y);
            //Time.deltaTime 物理时钟的运行百分比   
            //x                              z
            anim.SetFloat("runing", Mathf.Abs(facedirection));  //abs 绝对值
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }                                           //x     y    z

        #endregion

        #region 角色跳跃
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpfore );

            anim.SetBool("jumping", true);
        }
        #endregion


    }
    void SwitchAnim()
    {
         anim.SetBool("idle", true);
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y<0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);

        }
    }

}









