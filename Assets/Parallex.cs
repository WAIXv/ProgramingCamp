using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    private Vector3 StartPos;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private Vector2 MoveRate;
    [SerializeField] private bool ForcingCamera;
    // Start is called before the first frame update
    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!ForcingCamera)
        {
            transform.position = new Vector3(StartPos.x + cameraPos.position.x * MoveRate.x, StartPos.y + cameraPos.position.y * MoveRate.y, StartPos.z);
        }
        else
        {
            transform.position = new Vector3(cameraPos.position.x,cameraPos.position.y, StartPos.z);
        }
    }
}
