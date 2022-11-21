using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox_Goblin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.isTrigger == false)
        {
            FSM_Knight target = collision.gameObject.GetComponent<FSM_Knight>();
            target.SendMessage("DecreaseHP", 30);
        }
    }
}
