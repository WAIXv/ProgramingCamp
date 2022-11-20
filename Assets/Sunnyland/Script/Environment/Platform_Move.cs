using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Platform_Move : MonoBehaviour
{

    public LayerMask empty;
    public Transform Left, Right;
    private Collider2D coll;

    public float PlatformSpeed;
    private bool FacedRight = true;

    
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();   //平台移动
    }
    void Move()     //平台移动
    {
        //待实现..
        float speed = PlatformSpeed * Time.deltaTime;
        if (transform.position.x > Right.position.x)
        {
            FacedRight = false;
        }
        else if(transform.position.x < Left.position.x)
        {
            FacedRight = true;
        }
        if(FacedRight)                          //左转
        {
            transform.Translate(speed, 0, 0);
        }
        else                                    //右转
        {
            transform.Translate(-speed, 0, 0);
        }

    }
}
