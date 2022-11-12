using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletmembers : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    //ËÙ¶È
    public void SetSpeed(Vector2 direciton)
    {
        rb.velocity = new Vector2(direciton.x * speed, direciton.y * speed);
        //  StartCoroutine(DestroyObj((float)1);
    }
    //´Ý»Ù
    IEnumerable DestroyObj(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
