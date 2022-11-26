using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    private Animator anim;
    public int health;
    public float Speed;
    private Rigidbody2D rb;
    public float leftDistance, rightDistance;
    private float left, right;
    public bool FaceRight = true;
    public float DieTime;
    public int score;
    // Start is called before the first frame update
    private bool HaveTaken=false;
    private int healthyMax;
    public Image emeryHealthText;
    void Start()
    {
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
            playerc x = GameObject.FindGameObjectWithTag("Player").GetComponent<playerc>();
            x.collectionsget += score;
            Invoke("Killer", DieTime);//延时
            HaveTaken = true;
        }
    }
    void Killer()
    {
        Destroy(gameObject);
    }

}
