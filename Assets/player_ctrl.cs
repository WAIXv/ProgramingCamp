using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class player_ctrl : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D _Rigidbody2D;
    BoxCollider2D _Collider;
    GameObject visual;

    private Animator a;

    bool onGround = false;
    float jump_cool = 0f;
    float ground_tick = 0.12f;
    float move_v = 0f;
    static float stop_a = 32.0f;
    static float scale_x;
    bool d_r = true;



    void Start()
    {
        _Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        _Collider = this.gameObject.GetComponent<BoxCollider2D>();
        visual = GameObject.Find("visual");
        scale_x = visual.transform.localScale.x;
        a = visual.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((onGround || ground_tick > 0) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            onGround = false;
            doJump(15.0f);
            jump_cool = 0.2f;
        }
        if (jump_cool > 0f)
        {
            jump_cool -= Time.deltaTime;
        }


        if (Input.GetKey(KeyCode.A))
        {
            move_v = move_v < 0 ? 0 : move_v;
            move_v += stop_a * Time.deltaTime;
            if(move_v > 7f)
            {
                move_v = 7f;
            }
            d_r = false;
            a.SetBool("walk", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move_v = move_v > 0 ? 0 : move_v;
            move_v -= stop_a * Time.deltaTime;
            if (move_v < -7f)
            {
                move_v = -7f;
            }
            d_r = true;
            a.SetBool("walk", true);
        }
        else
        {
            if (math.abs(move_v) <= stop_a * Time.deltaTime)
            {
                move_v = 0f;
            }
            else
            {
                move_v -= stop_a * Time.deltaTime*(move_v > 0 ? 1:-1);
            }
            a.SetBool("walk", false);
        }
        setMovement(move_v);

        Vector3 vec = visual.transform.localScale;
        if(d_r)
        {
            vec.x += Time.deltaTime * scale_x * 18;
            if(vec.x > scale_x)
                vec.x = scale_x;
        }
        else
        {
            vec.x -= Time.deltaTime * scale_x * 18;
            if (vec.x < -scale_x)
                vec.x = -scale_x;
        }
        visual.transform.localScale = vec;
    }

    void FixedUpdate()
    {
        ground_tick -= Time.deltaTime;
        if (ground_tick <= 0) ground_tick = -0.1f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != _Collider) 
        { 
            onGround = true;
            a.SetBool("jump", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != _Collider)
        {
            onGround = false;
            ground_tick = 0.12f;
        }
    }


    private void doJump(float v)
    {
        Vector2 vec = _Rigidbody2D.velocity;
        vec.y = v;
        _Rigidbody2D.velocity = vec;
        a.SetBool("jump", true);
    }

    private void setMovement(float v)
    {
        Vector2 vec = _Rigidbody2D.velocity;
        vec.x = -v;
        _Rigidbody2D.velocity = vec;
    }

}
