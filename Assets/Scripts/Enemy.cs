using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected Animator anim;
    protected AudioSource deathAudio;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathAnim()
    {
        deathAudio.Play();
        anim.SetTrigger("Death");
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
