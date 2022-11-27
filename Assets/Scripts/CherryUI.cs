using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CherryUI : MonoBehaviour
{
    public int startCherryNum;
    public Text CherryNum;

    public static int CurrentCherryNum;
    // Start is called before the first frame update
    void Start()
    {
        CurrentCherryNum = startCherryNum;
    }

    // Update is called once per frame
    void Update()
    {
        CherryNum.text = CurrentCherryNum.ToString();
    }
}
