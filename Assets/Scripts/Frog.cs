using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Rigidbody2D Rb;
    [SerializeField] private Transform leftside, rightside;
    private float left, right;
    [SerializeField]private float Speed;
    private bool Faceleft = true;

    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        left = leftside.position.x;
        right = rightside.position.x;
        Destroy(leftside.gameObject);
        Destroy(rightside.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Faceleft)
        {
            Rb.velocity = new Vector2(-Speed, Rb.velocity.y);
            if (transform.position.x < left)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            Rb.velocity = new Vector2(Speed, Rb.velocity.y);
            if (transform.position.x > right)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }
}
