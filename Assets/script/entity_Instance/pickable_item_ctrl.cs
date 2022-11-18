using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickable_item_ctrl : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.tag == "player_obj")
        {
            collision.gameObject.GetComponent<entity_Instance>().Heal(300f);
            GameObject.Destroy(gameObject);
        }
    }
}
