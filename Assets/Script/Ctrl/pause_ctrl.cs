using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pause_ctrl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image bgMask;
    [SerializeField]
    private Text txt;

    private bool pause = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.unscaledDeltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                pause = false;
                Time.timeScale = 1f;
            }
            else
            {
                pause = true;
                Time.timeScale = 0f;
            }
        }

        if (pause)
        {
            Color col = bgMask.color;
            col.a += 5.0f * dt;
            if (col.a >= 0.8f) col.a = 0.8f;
            bgMask.color = col;
            col = txt.color;
            col.a = bgMask.color.a/0.8f;
            txt.color = col;
        }
        else
        {
            Color col = bgMask.color;
            col.a -= 8.0f * dt;
            if (col.a < 0f) col.a = 0f;
            bgMask.color = col;
            col = txt.color;
            col.a = bgMask.color.a / 0.8f;
            txt.color = col;
        }

    }
}
