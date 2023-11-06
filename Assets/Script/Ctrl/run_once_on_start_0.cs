using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static S;

public class run_once_on_start_0 : MonoBehaviour
{
    [SerializeField]
    private Image img;
    [SerializeField] 
    private camera_ctrl cam_ctrl;

    bool once = true;

    // Start is called before the first frame update

    void Awake()
    {
        img.enabled = true;
    }

    void Start()
    {
        StartCoroutine(D(0.35f, 0.5f));
    }

    IEnumerator D(float t, float wait)
    {
        float timer = wait;
        //Time.timeScale = 0f;
        while (true)
        {
            yield return null;
            timer -= Time.unscaledDeltaTime;
            if (timer <= 0) break;
        }
        timer = t;
        Color col;
        while (true)
        {
            yield return null;

            timer -= Time.unscaledDeltaTime;
            
            col = img.color;
            col.a = timer / t;
            img.color = col;
            if (timer <= 0) break;
        }
        Time.timeScale = 1f;
        img.enabled = false;
    }

    public void RestartGame()
    {
        StartCoroutine(E(0.5f, 0.3f));
    }

    public bool WinOrLost()
    {
        return once && (cam_ctrl.Win || cam_ctrl.Lost);
    }

    IEnumerator E(float t, float wait)
    {
        img.enabled = true;
        once = false;
        float timer = wait;
        while (true)
        {
            yield return null;
            timer -= Time.unscaledDeltaTime;
            if (timer <= 0) break;
        }
        timer = t;
        Color col;
        while (true)
        {
            yield return null;

            timer -= Time.unscaledDeltaTime;

            col = img.color;
            col.a = 1 - timer / t;
            img.color = col;
            if (timer <= 0) break;
        }
        SceneManager.LoadScene("startSence");
    }
}
