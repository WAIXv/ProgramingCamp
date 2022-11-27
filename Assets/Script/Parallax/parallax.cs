using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] private Transform Cam;
    [SerializeField] private float moveRate;
    private float newSpeedX,newSpeedY;
    private float startPointX,startPointY;
     [SerializeField]private bool lockY;

    void Start()
    {
        startPointX = transform.position.x;
        startPointY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockY)
        {
            newSpeedX = startPointX + Cam.position.x * moveRate;  
            transform.position = new Vector2(newSpeedX, transform.position.y);
        }
        else
        {
            newSpeedY = startPointY + Cam.position.y * moveRate;
            transform.position = new Vector2(newSpeedX,newSpeedY);
        }
    }
}
