using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform follower;      //����Ŀ��

    void Update()
    {
        transform.position = new Vector3(follower.position.x,follower.position.y,transform.position.z);       //ʵ�ּ򵥵��������  ��ʵҲ���Խ��������Ϊplayer��������
    }
}
