using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagle : enemy
{
    [SerializeField] private Rigidbody2D rb;
    //private Collider2D coll;
    public float speed;
    public Transform topPosition, bottomPosition;
    private float topY, bottomY;
    private bool isUp=true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb= GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        topY = topPosition.position.y;
        bottomY = bottomPosition.position.y;
        Destroy(topPosition.gameObject);
        Destroy(bottomPosition.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        moveMent();
    }
    void moveMent()
    {
        if (isUp)
        {
            rb.velocity=new Vector2(rb.velocity.x, speed);
            if (transform.position.y > topY)
            {
                isUp=false;
            }
        }
        else
        {
            rb.velocity=new Vector2(rb.velocity.x,-speed);
            if (transform.position.y < bottomY)
            {
                isUp = true;
            }
        }
    }
}
