using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    /// <summary>
    /// �ƶ�����Ŀ��
    /// </summary>
    [SerializeField] private Transform target;
    /// <summary>
    /// �ƶ��������ʱ��
    /// </summary>
    [SerializeField] private float time;// Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        CameraFollow();
    }
    void Update()
    {
        
    }
    /// <summary>
    /// �������
    /// </summary>
    void CameraFollow()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector2 targetPos = target.position;
                transform.position = Vector2.Lerp(transform.position, targetPos, time);
            }
        }
    }
}
