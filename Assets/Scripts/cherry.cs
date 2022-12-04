using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherry : MonoBehaviour
{
    public void Death()
    {
        Destroy(gameObject);
        FindObjectOfType<NewBehaviourScript>().CherryCount();
    }
}
