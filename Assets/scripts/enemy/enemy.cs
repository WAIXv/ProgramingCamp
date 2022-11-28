using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    protected AudioSource deathAudio;
    protected Animator Anim;
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

   public void Death()
    {
        
        Destroy(gameObject);
    }

    public void Jumpon()
    {
        deathAudio.Play();
        Anim.SetTrigger("death");
    }
}


