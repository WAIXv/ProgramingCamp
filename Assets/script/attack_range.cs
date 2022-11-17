using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_range : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    [SerializeField]
    private string tag;
    // Start is called before the first frame update

    // Update is called once per frame
    void LateUpdate()
    {
        targets.Clear();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.tag == tag && targets.IndexOf(collision.gameObject) < 0)
            targets.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.gameObject.tag == tag && targets.IndexOf(collision.gameObject) >= 0)
            targets.Remove(gameObject);
    }
}
