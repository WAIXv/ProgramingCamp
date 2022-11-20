using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCtrl : MonoBehaviour
{
    public GameObject mySelf;
    public void DialogExit()
    {
       mySelf.SetActive(false);
    }
}
