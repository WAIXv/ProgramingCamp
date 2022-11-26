using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhealth : MonoBehaviour
{
    public float DieTime;
    public int HP;
    public int Blinks;
    public float Time;
    public bool isdied=false;
    // Start is called before the first frame update
    private Renderer myRender;
    void Start()
    {
        HealthBar.Healthmax = HP;
        HealthBar.HealthCurrent = HP;
        myRender = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void DamagePlayer(int damage)
    {
        if (HP - damage >= 0)
        {
            HP -= damage;
        }
        else
        {
            HP= 0;
        }
        HealthBar.HealthCurrent = HP;
        if (HP <= 0)
        {
            isdied = true;
            Invoke("Killer", DieTime);//ÑÓÊ±
        }
        else
        {
            BlinkPlayer(Blinks, Time);
        }
    }
    public void Recovey(int recovey)
    {
       
        if(HealthBar.HealthCurrent+recovey<= HealthBar.Healthmax)
        {
            HP += recovey;
            HealthBar.HealthCurrent += recovey;
        } else
        {
            HP = HealthBar.Healthmax;
            HealthBar.HealthCurrent = HealthBar.Healthmax;
        }
    }
    void Killer()
    {
        Destroy(gameObject);
    }
    void BlinkPlayer(int numBlinks,float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }
    IEnumerator DoBlinks(int numBlinks,float seconds)
    {
        for(int i = 0; i < numBlinks * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}
