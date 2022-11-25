using Assets;
using Assets.script;
using Assets.Script;
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

    public float AD = 10f;
    public float AP = 0f;
    public float ATK = 530;

    public float wait_for_death = 0;
    protected bool death = false;
    public Rigidbody2D rb;

    public MyUtils.Executer beforeDeath;
    public MyUtils.Executer onDamage;

    void Start()
    {
        health = max_health;
    }
    public virtual void Heal(float h)
    {
        health = math.min(health + h, max_health);
    }
    public virtual float PhysicsDamage(float d)//return damage real
    {
        if (death) return 0f;
        if (onDamage != null) onDamage();
        float tmp = math.max(d - AD, 50f);
        health -= tmp;
        if (health <= 0)
        {
            death = true;
            StartCoroutine(WaitForDeath());
        }
        return tmp;
    }
    public virtual float MagicDamage(float d)//return damage real
    {
        if (death) return 0f;
        if (onDamage != null) onDamage();
        float tmp = math.min(100f - AP, 90f) * d / 100f;
        health -= tmp;
        if (health <= 0)
        {
            death = true;
            StartCoroutine(WaitForDeath());
        }
        return tmp;
    }
    public virtual float RealDamage(float d)//return damage real
    {
        if (death) return 0f;
        if (onDamage != null) onDamage();
        health -= d;
        if (health <= 0)
        {
            death = true;
            StartCoroutine(WaitForDeath());
        }
        return d;
    }
    public float getMaxHealth()
    {
        return max_health;
    }
    public virtual void doDeath()
    {
        GameObject.Destroy(gameObject);
    }
    public virtual void Update()
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
        rb.velocity += vec;
    }
    public virtual bool isDeath() { return death; }
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
