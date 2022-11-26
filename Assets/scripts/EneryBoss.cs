using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class EneryBoss : MonoBehaviour
{
    public float speed;
    public float radius;
    // Start is called before the first frame update
    private Transform playertransform;
    private float transformY;
    void Start()
    {
        transformY = this.transform.position.y;
        playertransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playertransform != null)
        {
            float distance = (transform.position - playertransform.position).sqrMagnitude;
            if (distance < radius)
            {
               transform.position = UnityEngine.Vector2.MoveTowards(transform.position, playertransform.position, speed * Time.deltaTime);
            }
        }
    }
}
