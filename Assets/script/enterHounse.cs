using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterHounse : MonoBehaviour
{
    public GameObject enterDialog; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            enterDialog.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            enterDialog.SetActive(false);
    
    }
}
