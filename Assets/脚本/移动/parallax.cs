using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] Transform cam;//摄像机位置
    [SerializeField] float moveRate;//移动速率
    private float startPointX, startPointY;//背景位置的X、Y值
    [SerializeField] bool lockY;//是否锁定Y轴

    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Parallax();
    }
    void Parallax()
    {
        if (lockY)
        {
            transform.position = new Vector2(startPointX + cam.position.x * moveRate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startPointX + cam.position.x * moveRate, startPointY + cam.position.y * moveRate);
        }
    }
}
