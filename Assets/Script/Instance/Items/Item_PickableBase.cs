using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_PickableBase : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool OnPick(entity_Instance EI) {
        return true; // return shouldDestory
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.GetComponent<entity_Instance>())
        {
            if(OnPick(collision.gameObject.GetComponent<entity_Instance>()))
                GameObject.Destroy(gameObject);
        }
    }
}
