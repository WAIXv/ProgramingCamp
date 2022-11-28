using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] Transform cam;//�����λ��
    [SerializeField] float moveRate;//�ƶ�����
    private float startPointX, startPointY;//����λ�õ�X��Yֵ
    [SerializeField] bool lockY;//�Ƿ�����Y��

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
