using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry1 : MonoBehaviour
{
    [SerializeField] private PlayerCtrler PLCR;
    [SerializeField] private Collider2D ColCherry;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            PLCR.ScorePlus();
        }
    }
}
