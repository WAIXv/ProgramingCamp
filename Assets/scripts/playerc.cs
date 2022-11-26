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
    public int collectionsget = 0;//�ռ���
    int cut1 = 0;//��ʱ�������ֹ�ռ���ʱ�����������������������ռ���ֱ�Ӽ�2;//��ʱ�������ֹ�ܻ���ʱ����������������������HP-2
    public bool candoublejump;
    public Text score;//�Ʒ�ui
    private bool isHurt;//�ж��Ƿ����ˣ�Ĭ����false
    private float facedirection;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = collectionsget.ToString();
        if (!isHurt&&this.GetComponent<playerhealth>().isdied==false)
        {
            Movement();
        }
        falljudge();
      //  Attack();
        cut1++;
    }//������
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");
        if (facedirection != 0&& facedirection*this.transform.localScale.x<0)//����
        {
            transform.localScale = new Vector3(facedirection*Mathf.Abs( this.transform.localScale.x), this.transform.localScale.y, 1);
        }
        if (horizontalmove != 0)//�����ƶ�
        {
            rb.velocity = new Vector2(horizontalmove * v, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(horizontalmove));
            if (collbox.IsTouchingLayers(ground))
            {
                rb.velocity = new Vector2(0, 0);//����ͷ�����壬��ͷ�������ǽ��ϣ����΢���ٻ�ǽ����
            }


        }
        if (Input.GetButtonDown("Jump")&&coll.IsTouchingLayers(ground))//��Ծ
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
        }//����
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
        }else if (coll.IsTouchingLayers(ground) || collbox.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idel", true);
        }
        if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running",0);
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 0.58f&& rb.velocity.y<2f) {//��������˺þã�ԭ���벻����Ϊʲô����ʱ��û�ܻ�������ԭ����˲������ˣ���������˸�y������������
                anim.SetBool("hurt", false);
                isHurt = false;
            }
        }
        
    }//�����ж��������л�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collection"&&cut1>100)
        { 
            Destroy(collision.gameObject);
            collectionsget++;
            cut1=0;
            this.GetComponentInParent<playerhealth>().Recovey(2);
            score.text = collectionsget.ToString();
            FindObjectOfType<playerskill>().RecoveyMP(4);
        }
    }//�ռ���Ʒ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemies" || collision.gameObject.tag == "boss" && cut1>=400)
        {
            if (collision.gameObject.tag == "enemies")
            {
                this.GetComponent<playerhealth>().DamagePlayer(1);
                cut1 = 0;
                isHurt = true;
            }
            else
            {
                this.GetComponent<playerhealth>().DamagePlayer(3);
                cut1 = 0;
                isHurt = true;

            }
            if (anim.GetBool("falling"))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.8f * jumpforce);
            }
            else if (transform.position.x != collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2( -facedirection*1.2f ,  rb.velocity.y);
            }
        }
    }
}
