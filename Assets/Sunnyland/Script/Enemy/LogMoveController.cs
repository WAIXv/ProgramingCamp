using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LogMoveController : MonoBehaviour
{
    public Transform left,right;
    public float speed;

    private bool isLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.DetachChildren();
    }
    void Update()
    {
        if (transform.position.x < left.position.x)
        {
            isLeft = false;
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else if (transform.position.x > right.position.x)
        {
            isLeft = true;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        if(isLeft)
        {
            transform.Translate(-speed * Time.deltaTime,0,0);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime,0,0);
        }
    }
}
