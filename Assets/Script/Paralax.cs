using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private float rate;
    private Vector3 startPoint;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(startPoint.x + cam.position.x * rate, startPoint.y + cam.position.y, 0);
    }
}
