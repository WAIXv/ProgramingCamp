using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    private float transformY;
    private Animator anim;
    public int health;
    public GameObject sceneTransform;
    public float Speed;
    private Rigidbody2D rb;
    public float leftDistance, rightDistance;
    private float left, right;
    private bool FaceRight = true;
    public float DieTime;
    public int score;
    // Start is called before the first frame update
    private bool HaveTaken=false;
    private int healthyMax;
    public Image emeryHealthText;
    private Transform playertransform;
    public AudioSource deathaudio;
    private bool isdied=false;
    public GameObject exitController;
    void Start()
    {       // deathaudio = GetComponent<AudioSource>();
        transformY = this.transform.position.y;
        playertransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // emeryHealthText = GameObject.FindGameObjectWithTag("UI").GetComponent<Image>();
        healthyMax = health;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        left = transform.position.x - leftDistance;
        right = transform.position.x + rightDistance;
    }

    // Update is called once per frame
    void Update()
    {
            Movement();
            Isdie();
    }
    void Movement()
    {
        if (this.tag == "boss")
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
          //  transform.localPosition = new Vector3(transform.position.x, transformY, transform.position.z);
            if (anim.GetBool("wake"))
            {
                if (playertransform.position.x - transform.position.x < 0&&FaceRight==true)
                {
                    transform.localScale = new Vector3(-0.04f, 0.04f, 1);
                    FaceRight = false;
                }
                else if(playertransform.position.x - transform.position.x > 0 && FaceRight == false)
                {
                    transform.localScale = new Vector3(0.04f, 0.04f, 1);
                    FaceRight = true;
                }
                
            }
        }
        if (this.tag == "enemies") {
        if (Mathf.Abs(rb.velocity.x)<1)//防止给它撞停了
        {
            if (FaceRight)
            {
                rb.velocity = new Vector2(Speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-Speed, rb.velocity.y);
            }
        }
            if (FaceRight)
            {
                rb.velocity = new Vector2(Speed, rb.velocity.y);
                if (transform.position.x > right)
                {
                    transform.localScale = new Vector3(-0.04f, 0.04f, 1);
                    FaceRight = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(-Speed, rb.velocity.y);
                if (transform.position.x < left)
                {
                    transform.localScale = new Vector3(0.04f, 0.04f, 1);
                    FaceRight = true;
                }
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (health - damage >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
            isdied = true;
        }
         float width = emeryHealthText.rectTransform.sizeDelta.x;
        emeryHealthText.rectTransform.sizeDelta = new Vector2((float)health/(float)healthyMax*width,0.07f);
    }
       void Isdie()
    {
        if (health <= 0 &&!HaveTaken)
        {
            rb.velocity = new Vector2(0, 0);
            anim.SetTrigger("die");
            deathaudio.Play();
            playerc x = GameObject.FindGameObjectWithTag("Player").GetComponent<playerc>();
            x.collectionsget += score;
            int y = x.collectionsget;
            if (y > 27)
            {
                sceneTransform.GetComponent<scenetransform>().enabled = true;
            }
            Invoke("Killer", DieTime);//延时
            if (this.tag == "boss")
            {
                exitController.SetActive(true);
            }
                HaveTaken = true;
        }
    }
    void Killer()
    {
        Destroy(gameObject);
    }

}
