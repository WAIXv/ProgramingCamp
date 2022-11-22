using Assets;
using Assets.script;
using Spine;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class entity_Instance : MonoBehaviour
{
    public float health;
    [SerializeField]
    protected float max_health = 1000f;
    public GameObject Obj;

    public EffectMgr EfcMgr = new EffectMgr();

    public float defense = 10;
    public float attack = 530;

    public float wait_for_death = 0;
    protected bool death = false;
    public Rigidbody2D rigidbody;

    public MyUtils.Executer beforeDeath;
    public MyUtils.Executer onDamage;

    void Start()
    {
        health = max_health;
    }

    public virtual float Heal(float h)
    {
        health = math.min(health + h, max_health);
        return 0;
    }
    
    public virtual float Damage(float d)//return damage real
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

    public virtual void doDeath()
    {
        GameObject.Destroy(gameObject);
    }

    void Update()
    {
        if(death) return;
        EfcMgr.Update(this);
        if (health <= 0)
        {
            death = true;
            StartCoroutine(WaitForDeath());
        }
    }

    public virtual void KnockBack(Vector2 vec)
    {
        rigidbody.velocity += vec;
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
