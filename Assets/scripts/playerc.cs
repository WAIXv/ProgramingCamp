using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerc : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float v = 4f;
    public float jumpforce;
    public float doublejumpforce;
    private Animator anim;
    public Collider2D coll;
    public Collider2D collbox;
    public LayerMask ground;
    public int collectionsget = 0;//收集数
    int cut = 0;//暂时用这个防止收集的时候人物两个刚体碰到，让收集数直接加2
    public bool candoublejump;
    public Text score;//计分ui
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        falljudge();
      //  Attack();
        cut++;
    }//主函数
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        if (facedirection != 0&& facedirection*this.transform.localScale.x<0)//朝向
        {
            transform.localScale = new Vector3(facedirection*Mathf.Abs( this.transform.localScale.x), this.transform.localScale.y, 1);
        }
        if (horizontalmove != 0)//控制移动
        {
            rb.velocity = new Vector2(horizontalmove * v, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(horizontalmove));
            if (collbox.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(0, 0);//拉长头部刚体，用头部检测贴墙，希望轻微减少滑墙现象
            }


        }
        if (Input.GetButtonDown("Jump")&&coll.IsTouchingLayers(ground))//跳跃
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce );
            anim.SetBool("jumping", true);
            candoublejump = true;
        }else if (candoublejump == true && Input.GetButtonDown("Jump"))
        {
            Vector2 doublejumpvel = new Vector2(rb.velocity.x, doublejumpforce);
            rb.velocity = Vector2.up * doublejumpvel;
            candoublejump = false;
        }
        }//动作
  /*  void Attack()
    {
        if (Input.GetButtonDown("attack")){
            anim.SetTrigger("attack");
        }
    }*/
    void falljudge()
    {
        anim.SetBool("idel", false);
        if (anim.GetBool("jumping") && rb.velocity.y <= 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }else if(coll.IsTouchingLayers(ground)){
            anim.SetBool("falling", false);
            anim.SetBool("idel", true);
        }
    }//下落判定及动画切换
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collection"&&cut>100)
        { 
            Destroy(collision.gameObject);
            collectionsget++;
            cut=0;
            score.text = collectionsget.ToString();
        }
    }//收集物品
}
