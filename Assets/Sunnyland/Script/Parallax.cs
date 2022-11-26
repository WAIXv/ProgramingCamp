using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Parallax : MonoBehaviour
{
    public Transform cam;
    public float moveRate;
    public bool lockY;//false

    private float startPointX, startPointY;

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
            transform.position = new Vector2(startPointX + cam.position.x * moveRate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startPointX + cam.position.x * moveRate, startPointY + cam.position.y * moveRate );
        }
    }
}
