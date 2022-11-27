using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOption : MonoBehaviour
{
    public Slider slider;
    public float Vol;
    static GameOption _instance;
    // public static GameOption instance
    // {
    //     get
    //     {
    //         if(_instance = null)
    //         {
    //             _instance = FindObjectOfType<GameOption>();
    //             DontDestroyOnLoad(_instance.gameObject);
    //         }
    //         return _instance;
    //     }
    // }
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if(this != _instance)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        Vol=slider.value;
    }
}
