using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform character;
    void Update()
    {
        transform.position = new Vector3(character.position.x, 0, -10f);
    }
}
