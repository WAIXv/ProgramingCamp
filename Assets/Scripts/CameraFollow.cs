using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public float smoothing;//ƽ������

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (target != null && transform.position != target.position)
        {
            Vector3 targetPost = target.position;
            transform.position = Vector3.Lerp(transform.position, targetPost, smoothing);//���Բ�ֵ
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
