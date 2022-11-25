using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_manager : MonoBehaviour
{
    public static AudioSource audioSrc;
    public static AudioClip shoot_bullet;
    public static AudioClip shoot_stair;
    public static AudioClip Enemy_death;
    public static AudioClip hit;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc =  GetComponent<AudioSource>();
        shoot_bullet = Resources.Load<AudioClip>("SoundFolder/DM-CGS-48");
        shoot_stair = Resources.Load<AudioClip>("SoundFolder/DM-CGS-47");
        Enemy_death = Resources.Load<AudioClip>("SoundFolder/Enemy_Death");
        hit= Resources.Load<AudioClip>("SoundFolder/Hit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayShootBullet()
    {
        audioSrc.PlayOneShot(shoot_bullet);
    }

    public static void PlayShootStair()
    {
        audioSrc.PlayOneShot(shoot_stair);
    }

    public static void Enemy_dead()
    {
        audioSrc.PlayOneShot(Enemy_death);
    }

    public static void hurted()
    {
        audioSrc.PlayOneShot(hit);
    }

    
}
