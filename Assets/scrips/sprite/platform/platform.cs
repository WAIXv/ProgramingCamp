using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform endpoint;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float speedx;
    // Update is called once per frame
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();    
    }
    void Update()
    {
        if(transform.position.x<endpoint.position.x)
        transform.position += new Vector3(speedx,0f,0f);
        body.velocity = new Vector2(0f,0f);
    }
}
