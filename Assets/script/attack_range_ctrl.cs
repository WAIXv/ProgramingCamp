using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_range_ctrl : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public bool hited = false;
    // Start is called before the first frame update
    void Start()
    {
        targets.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        hited = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player" && targets.IndexOf(collision.gameObject) == -1)
        { 
            targets.Add(collision.gameObject);
            hited = true;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            targets.Remove(collision.gameObject);
            //print(targets.Count);
        }
    }

}
