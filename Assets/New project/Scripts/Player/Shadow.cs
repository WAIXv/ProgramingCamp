using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;
    private Color color;
    [Header("时间控制")]
    [SerializeField] private float activeTime;
    [SerializeField] private float activeStart;
    [Header("不透明度控制")]
    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;

        transform.position = player.position;
        transform.rotation = player.rotation;
        transform.localScale = player.localScale;

        activeStart = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        alpha *= alphaMultiplier;

        color = new Color(0.8f, 1, 0.8f, alpha);

        thisSprite.color = color;

        if (Time.time > activeStart + activeTime)
        {
            //返回对象池
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
