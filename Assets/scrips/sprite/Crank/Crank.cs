using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    [SerializeField] private GameObject Step;
    [SerializeField] private GameObject crank_down;
    // Start is called before the first frame update
    void Awake()
    {
        Step = GameObject.Find("memberList").transform.Find("Step").gameObject;
        crank_down = GameObject.Find("Crank").transform.Find("crank-down").gameObject;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                crank_down.SetActive(true);
                Step.SetActive(true);
                audio_manager.PlayShootStair();
                audio_manager.playCrank();
                Destroy(gameObject);
            }
        }
    }
}
