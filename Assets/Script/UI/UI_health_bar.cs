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
    private Image Img_HP;
    [SerializeField]
    private Image SkillIconObj;
    [SerializeField]
    private GameObject SkillIconBlackMask;
    [SerializeField]
    private GameObject SkillIconEnergyMask;
    [SerializeField]
    private GameObject SkillIconTimeingMask;
    [SerializeField]
    private GameObject SkillReadyObj;
    public camera_ctrl camera;
    private player_Instance EI;

    // Start is called before the first frame update
    void Start()
    {
        _Init();
    }

    public void _Init()
    {
        EI = camera.player.GetComponent<player_Instance>();
        Img_HP = Internal.GetComponent<Image>();
        Texture2D texture = camera.player.GetComponent<player_Instance>().Skill.Icon;
        SkillIconObj.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
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
        Color col = Img_HP.color;
        col.r = 1f - tmp.x;
        col.g = tmp.x;
        Img_HP.color = col;
        health_txt.text = (int)math.max(math.min(EI.health, EI.getMaxHealth()),0) + "/" + (int)EI.getMaxHealth();
        Internal.transform.localScale = tmp;

        bool avilable = EI.Skill.isAvailable();
        rate = math.max(0f, math.min(EI.Skill.skillPointBuffer / EI.Skill.skillPoint,1.0f));
        RateObj(SkillIconEnergyMask, rate);

        rate = math.max(0f, math.min(EI.Skill.SkillTimeRate, 1.0f));
        RateObj(SkillIconTimeingMask, rate);

        SkillIconTimeingMask.SetActive(rate>0);
        SkillIconBlackMask.SetActive(!avilable && rate <= 0);
        SkillReadyObj.SetActive(avilable);
    }

    private void RateObj(GameObject obj,float rate)
    {
        Vector3 tmp = obj.transform.localScale;
        tmp.y = rate;
        obj.transform.localScale = tmp;
    }



}
