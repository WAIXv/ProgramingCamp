using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int MaxLife;
    [SerializeField] private int Life;
    [SerializeField] private int Damage;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color color;
    [SerializeField] private float flashTime;
    [SerializeField] private GameObject Drops;
    // Start is called before the first frame update
    protected void Start()
    {
        Life = MaxLife;
        color = spriteRenderer.color;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Life <= 0)
        {
            Instantiate(Drops, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }
    public void takeDamaeg(int damage)
    {
        flashColor(flashTime);
        Life -= damage;
    }

    public void flashColor(float time)
    {
        spriteRenderer.color = Color.red;
        Invoke("reset", time);
    }

     void reset()
    {
        spriteRenderer.color = color;
    }
}
