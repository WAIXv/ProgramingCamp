using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start_page_ctrl : MonoBehaviour
{
    [SerializeField]
    private Text press_to_login;
    [SerializeField]
    private GameObject prts_logo_0;
    [SerializeField]
    private Image prts_logo_0_1;
    [SerializeField]
    private Image prts_logo_0_2;
    [SerializeField]
    private Image prts_logo_0_3;
    [SerializeField]
    private Image prts_logo_0_4;
    [SerializeField]
    private GameObject prts_logo_1;
    [SerializeField]
    private GameObject prts_logo_2;
    [SerializeField]
    private Text prts_logo_2_1;
    [SerializeField]
    private Text prts_logo_2_2;
    [SerializeField]
    private GameObject prts_logo_2_3;
    [SerializeField]
    private GameObject prts_logo_3;
    [SerializeField]
    private GameObject prts_line1;
    [SerializeField]
    private GameObject prts_line2;

    private float timer = 0;
    private Vector2 offset;

    private bool loaded = false;

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        int w = cam.pixelWidth;
        int h = cam.pixelHeight;
        offset = new Vector2(w / 2, h / 2);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Color color = press_to_login.color;
        color.a = timer >= 1 ? 2-timer : timer;
        press_to_login.color = color;
        Vector2 mouse_pos = Input.mousePosition;
        mouse_pos -= offset;
        prts_logo_0.transform.localPosition = mouse_pos * 0.014f;
        prts_logo_1.transform.localPosition = mouse_pos * 0.0065f;
        prts_logo_2.transform.localPosition = -mouse_pos * 0.01f;
        prts_logo_3.transform.localPosition = mouse_pos * 0.002f;

        if (timer >= 2.0f) timer=0;

        if (Input.GetMouseButtonDown(0) && !loaded) {
            StartCoroutine(Load(0.2f,0.05f,0.4f,0.3f));
        }
    }


    IEnumerator Load(float t1, float t2,float t3, float t4)
    {
        float timer = t1;
        loaded = true;
        Vector2 ls1 = new Vector2(1, 1);
        Vector2 ls2 = new Vector2(1, 1);
        Color col;

        prts_logo_3.SetActive(false);
        prts_logo_2_3.SetActive(false);

        while (true)
        {
            yield return new WaitForEndOfFrame();
            
            timer -= Time.deltaTime;
            ls1.x = timer / t1;

            prts_logo_0.transform.localScale = ls2 * (4f - 3f * timer / t1);
            prts_logo_2.transform.localScale = ls2 * (2f - timer / t1);

            col = prts_logo_0_1.color;
            col.a = timer / t1;
            prts_logo_0_1.color = col;
            prts_logo_0_2.color = col;
            prts_logo_0_3.color = col;
            prts_logo_0_4.color = col;

            col = prts_logo_2_1.color;
            col.a = timer / t1;
            prts_logo_2_1.color = col;
            prts_logo_2_2.color = col;

            if (timer <= 0) break;
        }
        timer = t3;

        yield return new WaitForSeconds(t2);

        while (true)
        {
            yield return new WaitForEndOfFrame();

            timer -= Time.deltaTime;
            ls1.x = (1 - timer / t1) * 1.285f;
            prts_line1.transform.localScale = ls1;
            prts_line2.transform.localScale = ls1;
            if (timer <= 0) break;
        }

        yield return new WaitForSeconds(t4);

        SceneManager.LoadScene("MainScene");
    }


}
