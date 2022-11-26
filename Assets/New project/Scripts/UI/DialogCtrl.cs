using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCtrl : MonoBehaviour
{
    public GameObject gameObj;
    public void DialogExit()
    {
       gameObj.SetActive(false);
    }
}
