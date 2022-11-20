using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XunLuo : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [SerializeField] float speed;
    private bool faceLeft = true;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
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
