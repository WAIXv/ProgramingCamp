using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform tsf = gameObject.transform;
        RaycastHit2D rch2 = Physics2D.BoxCast(tsf.position, tsf.localScale, 0, Vector2.down,1f,LayerMask.GetMask("World"));
        Debug.Log(rch2.collider.gameObject.name);
    }
}
