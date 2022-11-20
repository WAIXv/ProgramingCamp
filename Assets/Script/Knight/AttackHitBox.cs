using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FSM_Goblin target = collision.gameObject.GetComponent<FSM_Goblin>();
            target.SendMessage("DecreaseHP", 10);
            Debug.Log("Hit Enemy");
        }
    }
}
