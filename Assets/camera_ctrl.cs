using Assets;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;

using static Assets.EntityStates;

public class camera_ctrl : MonoBehaviour
{
    private Camera cam;
    private GameObject player;
    private GameObject bg;

    private GameObject danger_layer;
    private GameObject lost_layer;

    public float edge = 10f;

    static float a_x = 2.2f; 
    static float a_y = 1.9f;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        player = GameObject.Find("player");
        bg = GameObject.Find("bg");
        danger_layer = GameObject.Find("danger");
        lost_layer = GameObject.Find("lost");

    }

    // Update is called once per frame

    void FixedUpdate()
    {
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
        tmp = pos.y * a_y;
        tmp = (math.abs(tmp) <= 0.1f ? 0f : tmp) * dt;

        if (math.abs(cam_pos.y - tmp) >= 0.0001f)
            cam_pos.y += tmp;
        else
            cam_pos.y = p_pos.x;

        if (cam_pos.y <= 0.8f) cam_pos.y = 0.8f;

        cam.transform.position = cam_pos;
    }

    void Update()
    {
        Color col = danger_layer.GetComponent<SpriteRenderer>().color;
        if (player.transform.position.y < -5f)
        {
            col.a += Time.deltaTime * 1.5f;
            col.a = math.min(1.0f,col.a);

        }
        else
        {
            col.a -= Time.deltaTime * 1.5f;
            col.a = math.max(0.0f, col.a);
        }

        danger_layer.GetComponent<SpriteRenderer>().color = col;
        lost_layer.GetComponent<SpriteRenderer>().color = col;
    }
}
