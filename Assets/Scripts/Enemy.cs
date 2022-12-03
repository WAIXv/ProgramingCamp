using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;
    protected AudioSource DeathAudio;
    
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        DeathAudio = GetComponent<AudioSource>();
    }

    public void Death()
    {
        
        Destroy(gameObject);
    }

    public void JumpOn()
    {
        Anim.SetTrigger("death");
        DeathAudio.Play();
    }


}
