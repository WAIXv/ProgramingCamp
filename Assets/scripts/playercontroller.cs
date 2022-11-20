using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    private  Rigidbody2D player;
    private Animator anim;
    [SerializeField] private Collider2D feet;
    [SerializeField] private float speedx;
    [SerializeField] private float jumpforce;
    [SerializeField] private LayerMask ground;
    private int cherry = 0;
    private int gem = 0;
    private int jumpchance;
    
    [SerializeField] private Text Cherrynum;
    [SerializeField] private Text Gemnum;


    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        movement();
        SwitchAnim();
    }

    //��ɫ�ƶ�
    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        
        //��ɫ�ƶ�
        if  (horizontalmove != 0)
        {
            player.velocity = new Vector2 (horizontalmove*speedx, player.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        
        //תͷ
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        if (anim.GetBool("onground"))
            jumpchance = 1;
        //��ɫ��Ծ
        if (Input.GetButtonDown("Jump")&& jumpchance > 0)
        {
            player.velocity = new Vector2 (player.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
            if(!anim.GetBool("onground"))
                jumpchance -=1;
            anim.SetBool("onground",false);
        }
    }

    //�仯����
    void SwitchAnim()
    {
        anim.SetBool("idle", false);

        if(anim.GetBool("jumping"))
        {
            if(player.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if(feet.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle",true);
            anim.SetBool("onground", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "cherry")
        {
            Destroy(collision.gameObject);
            cherry += 1;
            Cherrynum.text = cherry.ToString();
        }
        if (collision.tag == "gem")
        {
            Destroy(collision.gameObject);
            gem += 1;
            Gemnum.text = gem.ToString();
        }
    }

    //���ܵ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(feet.IsTouching(collision.collider))
        {
            if (collision.gameObject.tag == "enemy")
            {
                Destroy (collision.gameObject);
                jumpchance +=1;
            }
        }
    }
}
