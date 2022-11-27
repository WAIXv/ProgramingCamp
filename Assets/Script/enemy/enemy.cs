using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    protected Animator animi;
    protected AudioSource DeathAudio;
    protected virtual void Start()
    {
        animi = GetComponent<Animator>();
        DeathAudio = GetComponent<AudioSource>();
    }
    public void death()
    {
        Destroy(gameObject);

    }
    public void JumpOn()
    {
        DeathAudio.Play();
        animi.SetTrigger("death");
    }
}
