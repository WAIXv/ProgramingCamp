using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Collider2D playerColl2;
    [SerializeField] private PlayerCtrler PCr;
    // Start is called before the first frame update
    void Start()
    {
        playerColl2=GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PCr.LayerCheck(playerColl2);
    }
    private void Update()
    {
        
    }
}
