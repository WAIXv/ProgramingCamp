using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MPBar : MonoBehaviour
{
    public Text MPText;
    public static int MPCurrent;
    public static int MPmax;

    // Start is called before the first frame update
    private Image MPsBar;
    void Start()
    {
        MPsBar = GetComponent<Image>();
        // HealthCurrent = Healthmax;
    }

    // Update is called once per frame
    void Update()
    {
        MPsBar.fillAmount = (float)MPCurrent / (float)MPmax;
        MPText.text = "MP:  " + MPCurrent.ToString() + '/' + MPmax.ToString();
    }
}
