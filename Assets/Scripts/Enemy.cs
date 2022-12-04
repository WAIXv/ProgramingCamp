using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected Animator Anim;
    protected AudioSource deathAudio;
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }
    public void Death()
    {
        GetComponent<Collider2D>().enabled= false;
        Destroy(gameObject);
    }
    public void JumpOn()
    {
        Anim.SetTrigger("death");
        deathAudio.Play();
    }

}
