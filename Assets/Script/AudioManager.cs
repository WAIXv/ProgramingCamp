using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource attack;
    public AudioSource kill;
    public AudioSource death;
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        else instance = this;
    }

    public void PlayAttackMusic()
    {
        attack.Play();
    }

    public void PlayKillMusic()
    {
        kill.Play();
    }
    public void PlayDeathMusic()
    {
        death.Play();
    }
}
