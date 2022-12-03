using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour
{
    private Rigidbody2D myRigidbody;//刚体
    private bool faceLeft = true;//判断是否面向左
    [SerializeField] float speed;//速度
    [SerializeField] Transform leftPoint;//左点
    [SerializeField] Transform rightPoint;//右点
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        pat();
    }
    /// <summary>
    /// 巡逻
    /// </summary>
    void pat()
    {
        if (faceLeft)
        {
            myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
            if (transform.position.x < leftPoint.position.x)
            {
                transform.localScale = new Vector3(-1,1,1);
                faceLeft = false;
            }
        }
        else
        {
            myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
            if (transform.position.x > rightPoint.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = rightPoint;

            }
        }

    }
}
