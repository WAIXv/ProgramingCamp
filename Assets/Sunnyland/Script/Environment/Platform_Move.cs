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
        Move();   //ƽ̨�ƶ�
    }
    void Move()     //ƽ̨�ƶ�
    {
        //��ʵ��..
        float speed = PlatformSpeed * Time.deltaTime;
        if (transform.position.x > Right.position.x)
        {
            FacedRight = false;
        }
        else if(transform.position.x < Left.position.x)
        {
            FacedRight = true;
        }
        if(FacedRight)                          //��ת
        {
            transform.Translate(speed, 0, 0);
        }
        else                                    //��ת
        {
            transform.Translate(-speed, 0, 0);
        }

    }
}
