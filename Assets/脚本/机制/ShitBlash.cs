using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitBlash : MonoBehaviour
{
    /// <summary>
    /// 销毁的时间
    /// <summary>
    [SerializeField] private float blashTime;
    /// <summary>
    /// 要生成的粒子效果
    /// </summary>
    [SerializeField] private GameObject shitParticle;// Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, blashTime);//若子弹没碰撞到物体，一段时间后自动销毁
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 检测子弹是否与除玩家外的物体碰撞
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 被销毁时生成的特效
    /// </summary>
    private void OnDestroy()
    {
        Instantiate(shitParticle, transform.position, Quaternion.identity);
    }
}
