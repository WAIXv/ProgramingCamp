using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");//方向
        Vector2 playerVel = new Vector2(moveDir * runSpeed, rb.velocity.y);//速度
        rb.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;//x轴有速度
        anim.SetBool("Run", playerHasXAxisSpeed);
    }
}
