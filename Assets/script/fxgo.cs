using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fxgo : MonoBehaviour
{
    public float speed=5;
    //public GameObject Bulletpre;
    private Animator anim;//在开头加上[SerializeField]变为可见不可改
    private Rigidbody2D rb;
    public Collider2D coll;
    public LayerMask ground;
    public float jumpforce;
    [SerializeField] private int cherry=0;
    public Text cherryNumber;
    private bool isHurt;
    void Start()
    {
        //获取组件
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
        //fire();
    }
    private void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        //移动
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        //变向
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
            
        }
        //跳跃
        if (Input.GetButtonDown("Jump")&& coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
           // animator.SetBool("jumping", true);
        }
    }
    //变换动画
    private void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);

        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }
    //触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集
        if (collision.tag == "collection")
        {
            Destroy(collision.gameObject);
            cherry++;
            cherryNumber.text=cherry.ToString();
        }
        if (collision.tag == "deadLine")
        {
            //延迟
            Invoke("reStart", 1.5f);
            
        }
    }
    //消灭敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (anim.GetBool("falling"))
            {
                Destroy(collision.gameObject);//销毁后小跳
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
               
                rb.velocity = new Vector2(-10,rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                
                rb.velocity=new Vector2(10,rb.velocity.y);
                isHurt = true;
            }
            
        }

    }
    //重启场景
    private void reStart()
    {
        //重新加载某场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //射击
    /*void fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
              int number = 1;
              Debug.Log(number);
              number++;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos = transform.position;
            //   int randx = Random.Range(0, 360);//考虑设置随机方向
            Vector2 direction = (mousePos - pos).normalized;
            GameObject bullet = Instantiate(Bulletpre, pos, Quaternion.identity);
            bullet.GetComponent<bulletmembers>().SetSpeed(direction);
        }
    }*/
}
