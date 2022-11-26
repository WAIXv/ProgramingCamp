using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private GameObject gameObj;
    [SerializeField] private PlatformEffector2D effector;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        PlatformDown();
    }
    private void PlatformDown()//���¼�����ƽ̨
    {
        if (Input.GetButtonDown("Crouch")) effector.rotationalOffset = 180;
        if (Input.GetButtonUp("Crouch"))effector.rotationalOffset = 0; 
    }
}
