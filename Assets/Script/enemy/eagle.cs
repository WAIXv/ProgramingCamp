using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class eagle : enemy
{
    [SerializeField] private Rigidbody2D Eagle;
    [SerializeField] private Transform toppoint, buttompoint;
    [SerializeField] private bool isUp = true;
    [SerializeField] private float Speed;
    //[SerializeField] private Collider2D coll;
    private float topY, buttomY;

    protected override void Start()
    {
        base.Start();

        transform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }
    void Movement()
    {
        if (isUp)
        {
            Eagle.velocity = new Vector2(Eagle.velocity.x, Speed);
            if (transform.position.y > toppoint.position.y)
            {
                isUp = false;
            }
        }
        else
        {
            Eagle.velocity = new Vector2(Eagle.velocity.x, -Speed);
            if (transform.position.y < buttompoint.position.y)
            {
                isUp = true;
            }

        }
    }
}