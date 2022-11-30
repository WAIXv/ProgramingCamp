using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_move : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool face_direction;
    [SerializeField] private Transform leftpoint;
    [SerializeField] private Transform rightpoint;
    // Start is called before the first frame update
    void Start()
    {
        face_direction = true;
    }

    // Update is called once per frame
    void Update()
    {
        patrol();
    }

    private void patrol()
    {
        if(face_direction)
        {
            transform.position += new Vector3(-speed*Time.deltaTime,0f,0f);
        }
        else
        {
            transform.position += new Vector3(speed*Time.deltaTime,0f,0f);
        }
        if((transform.position.x<leftpoint.position.x&&face_direction)||(transform.position.x>rightpoint.position.x&&!face_direction))
        {
            face_direction =!face_direction;
        }
    }
}
