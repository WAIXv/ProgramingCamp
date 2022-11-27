using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    protected Animator Anim;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);

    }
    public void jumpOn()
    {
        Anim.SetTrigger("death");
    }
}
