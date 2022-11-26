using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egale : Enemy
{
    [SerializeField] private float up, down;
    [SerializeField] private bool reachTop;
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        up = transform.position.y + 3;
        down=transform.position.y-3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeathing)
        {
            EgaleMove();
        }
    }
    private void EgaleMove()
    {
        if (transform.position.y >= up) reachTop = true;
        if (transform.position.y <= down) reachTop = false;
        if (!reachTop)
        {
            transform.position += new Vector3(0, moveSpeed*Time.deltaTime);
        }
        if (reachTop)
        {
            transform.position+=new Vector3(0, -moveSpeed*Time.deltaTime);
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }
    private void BeforeDeath()
    {
        Destroy(enemyColl);
    }
}
