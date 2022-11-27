using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collection : MonoBehaviour
{
    // Start is called before the first frame update
    protected AudioSource collectAudio;
    protected virtual void Start()
    {
        collectAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    public void Collect()
    {
        collectAudio.Play();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
