using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuMusic: MonoBehaviour
{
    private GameOption _Option;
    public static AudioSource mainMenu_audio_Soc;
    public static AudioClip clickSound;
    public static AudioClip pointOnSound;
    
    // Start is called before the first frame update
    void Start()
    {
        mainMenu_audio_Soc =  GetComponent<AudioSource>();
        clickSound = Resources.Load<AudioClip>("SoundFolder/Hit");
        pointOnSound = Resources.Load<AudioClip>("SoundFolder/Step_grass");
        _Option = GameObject.Find("GameOption").GetComponent<GameOption>();
    }

    // Update is called once per frame
    void Update()
    {
        mainMenu_audio_Soc.volume = _Option.Vol;
        if(_Option.ifMute)
        mainMenu_audio_Soc.volume = 0;
    }

    public void play_clickSound()
    {
        mainMenu_audio_Soc.PlayOneShot(clickSound);
        Debug.Log(22);
    }

    public void play_pointOnSound()
    {
        mainMenu_audio_Soc.PlayOneShot(pointOnSound);
        Debug.Log(11);
    }
}
