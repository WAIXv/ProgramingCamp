using Assets;
using Unity.Mathematics;
using UnityEngine;


public class camera_ctrl : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    public GameObject player;
    public bool Lost = false;
    public bool Win = false;
    //private GameObject bg;

    [SerializeField]
    private GameObject danger_layer;
    [SerializeField]
    private GameObject lost_layer;

    public float edge = 10f;

    [SerializeField]
    private float offest_y;

    static float a_x = 2.2f; 
    static float a_y = 1.9f;

    [SerializeField]
    private GameObject audio_lost;
    [SerializeField]
    private GameObject audio_win;

    [SerializeField]
    private float min_y = -20f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (player == null)
        {
            Lost = true;
            return;
        }
        Vector3 cam_pos = cam.transform.position;
        Vector3 p_pos = player.transform.position;
        Vector3 pos = p_pos - cam_pos;
        float dt = Time.deltaTime;

        //ax
        float tmp = pos.x * a_x;

        tmp = (math.abs(tmp) <= 0.3f ? 0.0f : tmp) * dt;

        if (math.abs(cam_pos.x - tmp) >= 0.0001f)
            cam_pos.x += tmp;
        else
            cam_pos.x = p_pos.x;

        //ay
        pos.y -= offest_y;
        tmp = pos.y * a_y;
        tmp = (math.abs(tmp) <= 0.1f ? 0f : tmp) * dt;

        if (math.abs(cam_pos.y - tmp) >= 0.0001f)
            cam_pos.y += tmp;
        else
            cam_pos.y = p_pos.y;

        if (cam_pos.y <= min_y) cam_pos.y = min_y;

        cam.transform.position = cam_pos;
    }

    void Update()
    {
        Color col = danger_layer.GetComponent<SpriteRenderer>().color;
        if (Lost)
        {
            col.a += Time.deltaTime * 1.5f;
            col.a = math.min(1.0f,col.a);
#pragma warning disable CS0618 // 类型或成员已过时
            if (!audio_lost.active && !Win)
                audio_lost.SetActive(true);
#pragma warning restore CS0618 // 类型或成员已过时
        }
        else
        {
            col.a -= Time.deltaTime * 1.5f;
            col.a = math.max(0.0f, col.a);
        }

        if(Win)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            if (!audio_win.active && !Lost)
                audio_win.SetActive(true);
#pragma warning restore CS0618 // 类型或成员已过时
        }
        else
        {
            GameObject gobj = GameObject.FindGameObjectWithTag("mob_obj");
            if (gobj == null) Win = true;
        }

        danger_layer.GetComponent<SpriteRenderer>().color = col;
        lost_layer.GetComponent<SpriteRenderer>().color = col;
    }
}
