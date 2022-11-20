using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerFSM FSM = new PlayerFSM();
    // Start is called before the first frame update
    private void Awake()
    {
        FSM.parameter.animator= GetComponent<Animator>();
       // Instantiate(FSM);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }
}
