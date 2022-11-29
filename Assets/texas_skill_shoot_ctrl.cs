using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class texas_skill_shoot_ctrl : MonoBehaviour
{
    [SerializeField]
    private RawImage img;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setImgActive(bool a)
    {
        img.enabled = a;
    }

    public void setAlpha(float a)
    {
        img.material.SetFloat("_Alpha", a);
    }
}
