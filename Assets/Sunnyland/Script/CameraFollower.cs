using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform follower;      //跟随目标

    void Update()
    {
        transform.position = new Vector3(follower.position.x,follower.position.y,transform.position.z);       //实现简单的相机跟随  其实也可以将相机设置为player的子物体
    }
}
