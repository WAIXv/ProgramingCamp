using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public Vector2 minPos;
    public Vector2 maxPos;

    private float smoothing = 0.1f;//平滑因子

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (target != null && transform.position != target.position)
        {
            Vector3 targetPos = target.position;
            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);//限制相机移动范围
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);//线性插值
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCamPosLimit(Vector2 minPosition, Vector2 maxPosition)
    {
        minPos = minPosition;
        maxPos = maxPosition;
    }
}
