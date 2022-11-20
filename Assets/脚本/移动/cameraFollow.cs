using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    /// <summary>
    /// 移动至的目标
    /// </summary>
    [SerializeField] private Transform target;
    /// <summary>
    /// 移动到所需的时间
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
    /// 相机跟随
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
