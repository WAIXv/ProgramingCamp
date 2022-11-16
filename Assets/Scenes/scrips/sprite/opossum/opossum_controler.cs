using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opossum_controler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool find_target;
    [SerializeField] private bool face_direction;
    [SerializeField] private float jumpfoces;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] Transform leftpoint;
    [SerializeField] Transform rightpoint;
    [SerializeField] Transform attack_right;
    [SerializeField] Transform attack_left;
    [SerializeField] private player_controler player;
  
    // Start is called before the first frame update
    void Start()
    {
        face_direction = true;        
        find_target =false;
    }

    // Update is called once per frame
    
    void Update()
    {
        if(player.transform.position.x>=attack_left.position.x&&player.transform.position.x<=attack_right.position.x)
        {
            if(!find_target)
            {
                rb.velocity = new Vector3(rb.velocity.x,jumpfoces,0f);
                find_target =true;
            }
            battle();
        }
        else
        {
            patrol();
        }   
    }

    private void patrol()
    {
        find_target = false;
        if(face_direction)
        {
            transform.position += new Vector3(-1f,0f,0f)*speed*Time.deltaTime;
        }
        else if(!face_direction)
        {
            transform.position += new Vector3(1f,0f,0f)*speed*Time.deltaTime;
        }
        if((transform.position.x<=leftpoint.position.x&&face_direction)||(transform.position.x>=rightpoint.position.x)&&!face_direction)
        {
            transform.Rotate(0f,180f,0f);
            face_direction=!face_direction;
        }
    }
    private void battle()
    {
        face_direction = !(player.transform.position.x>transform.position.x);
        if(face_direction)
        {
            transform.position += new Vector3(-1f,0f,0f)*speed*2.6f*Time.deltaTime;
            transform.localEulerAngles = new Vector3 (0.0f,0.0f,0.0f); 
        }
        else if(!face_direction)
        {
            transform.position += new Vector3(1f,0f,0f)*speed*2.6f*Time.deltaTime;
            transform.localEulerAngles = new Vector3 (0.0f,180.0f,0.0f); 
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("player")&&other.GetType().ToString()=="UnityEngine.PolygonCollider2D")
        {
            if(player_controler.player_blood>0)
            {
                if(other.transform.position.x>transform.position.x)
                {
                    player.gethurt(damage,true);
                }
                else
                {
                    player.gethurt(damage,false); 
                }
            }      
        }
        else if(other.gameObject.CompareTag("player")&&other.GetType().ToString()=="UnityEngine.BoxCollider2D")
        {
            player.jump();
            Destroy(gameObject);
        }
    }



}
