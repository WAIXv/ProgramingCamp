using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootbullet : MonoBehaviour
{
    /// <summary>
    /// �ж��Ƿ��ڵ���
    /// </summary>
    private bool isGround;
    /// <summary>
    /// Ҫ������ӵ�
    /// </summary>
    [SerializeField] private GameObject shit;
    private BoxCollider2D myFeet;//��ȡ��ײ��// Start is called before the first frame update
    void Start()
    {
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        TakeShit();
    }
    /// <summary>
    /// �����ӵ�
    /// </summary>
    void TakeShit()
    {
        if (isGround==false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Instantiate(shit, transform.position, Quaternion.identity);
            }
        }
        
    }
    /// <summary>
    /// �ж��Ƿ�Ӵ�����
    /// </summary>
    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
