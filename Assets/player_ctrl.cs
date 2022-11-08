using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class player_ctrl : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D _Rigidbody2D;
    BoxCollider2D _Collider;

    bool onGround = false;
    float jump_cool = 0f;
    float ground_tick = 0.12f;
    float move_v = 0f;
    static float stop_a = 32.0f;

    void Start()
    {
        _Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        _Collider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            onGround = false;
            doJump(15.0f);
            jump_cool = 0.2f;
        }
        if (jump_cool >= 0.00001f)
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
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move_v = move_v > 0 ? 0 : move_v;
            move_v -= stop_a * Time.deltaTime;
            if (move_v < -7f)
            {
                move_v = -7f;
            }
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
        }
        setMovement(move_v);

    }

    void FixedUpdate()
    {
        ground_tick -= Time.deltaTime;
        if (ground_tick <= 0.0001f) onGround = false;
      
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (jump_cool <= 0.0001f && collision != _Collider) 
        { 
            onGround = true;
            ground_tick = 0.12f;
        }

    }


    private void doJump(float v)
    {
        Vector2 vec = _Rigidbody2D.velocity;
        vec.y = v;
        _Rigidbody2D.velocity = vec;
    }

    private void setMovement(float v)
    {
        Vector2 vec = _Rigidbody2D.velocity;
        vec.x = -v;
        _Rigidbody2D.velocity = vec;
    }

}
