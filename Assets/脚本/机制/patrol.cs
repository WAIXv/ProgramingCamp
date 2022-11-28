using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour
{
    private Rigidbody2D myRigidbody;//����
    private bool faceLeft = true;//�ж��Ƿ�������
    [SerializeField] float speed;//�ٶ�
    [SerializeField] Transform leftPoint;//���
    [SerializeField] Transform rightPoint;//�ҵ�
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
    /// Ѳ��
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
