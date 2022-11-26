using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]AudioSource audioSource;
    [SerializeField]AudioClip feedback,death;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayFeedbackSound()
    {
        audioSource.volume = 0.4f;
        audioSource.clip = feedback;
        audioSource.Play();
    }
    public void PlayDeathSound()
    {
        audioSource.volume = 1f;
        audioSource.clip=death;
        audioSource.Play();
    }
}
