using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootbullet : MonoBehaviour
{
    /// <summary>
    /// 判断是否在地面
    /// </summary>
    private bool isGround;
    /// <summary>
    /// 要射击的子弹
    /// </summary>
    [SerializeField] private GameObject shit;
    private BoxCollider2D myFeet;//获取碰撞机// Start is called before the first frame update
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
    /// 发射子弹
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
    /// 判断是否接触地面
    /// </summary>
    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
