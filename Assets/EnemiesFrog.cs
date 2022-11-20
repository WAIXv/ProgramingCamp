using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public Transform leftpoint, rightpoint;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

    }
}
