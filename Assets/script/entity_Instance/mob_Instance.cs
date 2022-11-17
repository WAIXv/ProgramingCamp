using Assets;
using Spine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class mob_Instance : MonoBehaviour
{
    public float health;
    [SerializeField]
    private float max_health;

    public float defense = 10;

    public float wait_for_death = 0;
    private bool death = false;
    public Rigidbody2D rigidbody;

    public MyUtils.Executer beforeDeath;
    public MyUtils.Executer onDamage;
    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
    }

    //return damage
    public float Damage(float d)
    {
        if (onDamage != null) onDamage();
        float tmp = math.max(d - defense, 50f);
        health -= tmp;
        return tmp;
    }

    public float getMaxHealth()
    {
        return max_health;
    }

    public void doDeath()
    {
        GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !death)
        {
            death = true;
            StartCoroutine(WaitForDeath());
        }
    }

    IEnumerator WaitForDeath()
    {
        while (true)
        {
            if(beforeDeath != null)
            {
                beforeDeath();
                beforeDeath = null;
            }
            yield return new WaitForSeconds(wait_for_death);
            doDeath();
        }
    }
}
