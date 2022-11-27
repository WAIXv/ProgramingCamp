using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuMusic: MonoBehaviour
{

    public static AudioSource mainMenu_audio_Soc;
    public static AudioClip clickSound;
    public static AudioClip pointOnSound;
    
    // Start is called before the first frame update
    void Start()
    {
        mainMenu_audio_Soc =  GetComponent<AudioSource>();
        clickSound = Resources.Load<AudioClip>("SoundFolder/Hit");
        pointOnSound = Resources.Load<AudioClip>("SoundFolder/Step_grass");
    }

    // Update is called once per frame
    void Update()
    {
        
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
