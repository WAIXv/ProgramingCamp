using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tab_Ctrl : MonoBehaviour
{
    [SerializeField]
    private GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        plane.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
