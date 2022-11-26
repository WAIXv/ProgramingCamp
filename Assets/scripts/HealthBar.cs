using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text HealthText;
    public static int HealthCurrent;
    public static int Healthmax;

    // Start is called before the first frame update
    private Image healthBar;
    void Start()
    {
        healthBar = GetComponent<Image>();
       // HealthCurrent = Healthmax;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)HealthCurrent / (float)Healthmax;
        HealthText.text = "HP: "+HealthCurrent.ToString() + '/' + Healthmax.ToString();
    }
}
