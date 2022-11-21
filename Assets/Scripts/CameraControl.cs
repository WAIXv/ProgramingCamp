using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;


    
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10f);


    }
}
