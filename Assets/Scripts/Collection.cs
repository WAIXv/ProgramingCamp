using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collection : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource collectAudio;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        collectAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    public void Collect()
    {
        collectAudio.Play();
        anim.SetTrigger("Collected");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
