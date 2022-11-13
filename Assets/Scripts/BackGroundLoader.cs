using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoader : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }
}
