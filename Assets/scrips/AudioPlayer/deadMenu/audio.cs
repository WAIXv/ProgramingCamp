using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public static AudioSource audioSrc;
    public static AudioClip BGM;
    // Start is called before the first frame update
    public static AudioClip clickSound;
    public static AudioClip pointOnSound;
    private GameOption _Option;
    void Awake()
    {
        audioSrc=GetComponent<AudioSource>();
        BGM=Resources.Load<AudioClip>("SoundFolder/BGM");
        audioSrc.PlayOneShot(BGM);
        clickSound = Resources.Load<AudioClip>("SoundFolder/Hit");
        pointOnSound = Resources.Load<AudioClip>("SoundFolder/Step_grass");
        _Option = GameObject.Find("GameOption").GetComponent<GameOption>();
        audioSrc.volume = _Option.Vol;
        if(_Option.ifMute) audioSrc.volume = 0;
    }
    public void Click()
    {
        audioSrc.PlayOneShot(clickSound);
    }
    public void PointOn()
    {
        audioSrc.PlayOneShot(pointOnSound);
    }

    
}
