using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class camera_ctrl : MonoBehaviour
{
    private Camera cam;
    private GameObject player;

    static float a_x = 2.2f; 
    static float a_y = 1.9f;

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        player = GameObject.Find("player");
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        Vector3 cam_pos = cam.transform.position;
        Vector3 p_pos = player.transform.position;
        Vector3 pos = p_pos - cam_pos;
        float dt = Time.deltaTime;

        //ax
        float tmp = pos.x * a_x;

        tmp = (math.abs(tmp) <= 0.1f ? 0.1f : tmp) * dt;

        if (math.abs(cam_pos.x - tmp) >= 0.0001f)
            cam_pos.x += tmp;
        else
            cam_pos.x = p_pos.x;


        //ay
        tmp = pos.y * a_y;
        tmp = (math.abs(tmp) <= 0.1f ? 0.1f : tmp) * dt;

        if (math.abs(cam_pos.y - tmp) >= 0.0001f)
            cam_pos.y += tmp;
        else
            cam_pos.y = p_pos.x;

        if (cam_pos.y <= 0.8f) cam_pos.y = 0.8f;

        cam.transform.position = cam_pos;

    }

    void Update()
    {
        
    }
}
