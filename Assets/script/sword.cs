using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sword : MonoBehaviour
{
    [SerializeField] public float speed = 2.0f;
    //private float smoothTime = 0.5f;
    private Rigidbody2D rb;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // dir();//方向
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("askjfdl");
            Vector2 mousePosition = Input.mousePosition;
            Vector2 nowPosition = transform.position;
            Vector2 direction = (mousePosition - nowPosition).normalized;
            rb.velocity = direction * speed;
        }
    }
    //向鼠标方向移动
    
    //计算方向
    /*void dir()
    {
       
        targetposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        swordDirection=(targetposition-transform.position).normalized;
        float angle =Mathf.Atan2(swordDirection.x, swordDirection.y)*Mathf.Rad2Deg;
        if (angle <= 90 && angle >= -90)
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        else if (angle > 90 || angle < -90)
        {
            transform.eulerAngles = new Vector3(-180, 0, -angle);
        }
       
    }*/
}
