using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Cam;
    public float moveRate;
    private float startPoint;
    void Start()
    {
        startPoint = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(startPoint + Cam.position.x * moveRate, transform.position.y);
    }
}
