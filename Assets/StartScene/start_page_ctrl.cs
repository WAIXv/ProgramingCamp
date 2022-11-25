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
    private GameObject prts_logo_1;
    [SerializeField]
    private GameObject prts_logo_2;
    [SerializeField]
    private GameObject prts_logo_3;

    private float timer = 0;
    private Vector2 offset;

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

        print(mouse_pos);

        prts_logo_0.transform.localPosition = mouse_pos * 0.014f;
        prts_logo_1.transform.localPosition = mouse_pos * 0.0065f;
        prts_logo_2.transform.localPosition = -mouse_pos * 0.01f;
        prts_logo_3.transform.localPosition = mouse_pos * 0.002f;

        if (timer >= 2.0f) timer=0;

        if (Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("MainScene"); 
        
        }
    }
}
