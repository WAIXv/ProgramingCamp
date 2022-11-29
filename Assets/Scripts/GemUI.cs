using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GemUI : MonoBehaviour
{
    public int startGemNum;
    public Text GemNum;

    public static int CurrentGemNum;
    // Start is called before the first frame update
    void Start()
    {
        CurrentGemNum = startGemNum;
    }

    // Update is called once per frame
    void Update()
    {
        GemNum.text = CurrentGemNum.ToString();
    }
}
