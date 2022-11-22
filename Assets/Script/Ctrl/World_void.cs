using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_void : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.isTrigger) {
            entity_Instance ei = collision.gameObject.GetComponent<entity_Instance>();
            if (ei != null) ei.health = -1f; 
        }
    }
}
