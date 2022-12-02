using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    [SerializeField] public static AudioSource audioEff;
    public static AudioClip clickSound;
    public static AudioClip pointOnSound;
    private GameOption _Option;
    // Start is called before the first frame update
    void Awake()
    {
        audioEff= GetComponent<AudioSource>();
        _Option = GameObject.Find("GameOption").GetComponent<GameOption>();
        clickSound = Resources.Load<AudioClip>("SoundFolder/Hit");
        pointOnSound = Resources.Load<AudioClip>("SoundFolder/Step_grass");
    }

    // Update is called once per frame
    void Update()
    {
        audioEff.volume = _Option.Vol;
        if(_Option.ifMute) audioEff.volume = 0;
    }

    public static void Click()
    {
    audioEff.PlayOneShot(clickSound);
    }
    public static void PointOn()
    {
     audioEff.PlayOneShot(pointOnSound);
    }
}
