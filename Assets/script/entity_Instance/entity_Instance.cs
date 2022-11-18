using Assets;
using Spine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class entity_Instance : MonoBehaviour
{
    public float health;
    [SerializeField]
    protected float max_health = 1000f;
    public GameObject Obj;

    public float defense = 10;

    public float wait_for_death = 0;
    protected bool death = false;
    public Rigidbody2D rigidbody;

    public MyUtils.Executer beforeDeath;
    public MyUtils.Executer onDamage;

    // Start is called before the first frame update
    void Start()
    {
        health = max_health;
    }

    public float Heal(float h)
    {
        health = math.min(health + h, max_health);
        return 0;
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
