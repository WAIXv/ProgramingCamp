using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitBlash : MonoBehaviour
{
    /// <summary>
    /// ���ٵ�ʱ��
    /// <summary>
    [SerializeField] private float blashTime;
    /// <summary>
    /// Ҫ���ɵ�����Ч��
    /// </summary>
    [SerializeField] private GameObject shitParticle;// Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, blashTime);//���ӵ�û��ײ�����壬һ��ʱ����Զ�����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// ����ӵ��Ƿ����������������ײ
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// ������ʱ���ɵ���Ч
    /// </summary>
    private void OnDestroy()
    {
        Instantiate(shitParticle, transform.position, Quaternion.identity);
    }
}
