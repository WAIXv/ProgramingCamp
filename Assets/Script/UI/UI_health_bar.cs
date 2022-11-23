using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UI_health_bar : MonoBehaviour
{
    [SerializeField]
    private GameObject Internal;
    [SerializeField]
    private Text health_txt;
    private Image IMG;
    public camera_ctrl camera;
    private entity_Instance EI;


    // Start is called before the first frame update
    void Start()
    {
        EI = camera.player.GetComponent<entity_Instance>();
        IMG = Internal.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float dr = Time.deltaTime*0.5f;
        float rate = math.min(math.max(EI.health / EI.getMaxHealth(), 0), 1.0f);
        Vector3 tmp = Internal.transform.localScale;
        tmp.x -= dr * (tmp.x > rate ? 1 : -1);
        if (math.abs(tmp.x - rate) < dr)
            tmp.x = rate;
        Color col = IMG.color;
        col.r = 1f - tmp.x;
        col.g = tmp.x;
        IMG.color = col;
        health_txt.text = (int)math.max(math.min(EI.health, EI.getMaxHealth()),0) + "/" + (int)EI.getMaxHealth();
        Internal.transform.localScale = tmp;
    }
}
