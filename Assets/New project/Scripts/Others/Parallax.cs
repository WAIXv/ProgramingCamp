using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform cameraTrans;
    [SerializeField] private float moveRate;
    [SerializeField] private float startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(startPoint+cameraTrans.position.x*moveRate, transform.position.y);
    }
}
