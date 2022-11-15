using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controler : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Transform leftpoint;
    [SerializeField] Transform rightpoint;
    [SerializeField] private player_controler player;
    // Start is called before the first frame update
    void Start()
    {
        face_direction = true;
        player = GameObject.FindGameObjectWithTag("player").GetComponent<player_controler>();
    }

    // Update is called once per frame
    void Update()
    {
        patrol();
    }
    [SerializeField] private bool face_direction;

    private void patrol()
    {
        if(face_direction)
        {
            rb.velocity = new Vector2(-speed,rb.velocity.y);
        }
        else if(!face_direction)
        {
            rb.velocity = new Vector2(speed,rb.velocity.y);
        }
        if((transform.position.x<=leftpoint.position.x&&face_direction)||(transform.position.x>=rightpoint.position.x)&&!face_direction)
        {
            transform.Rotate(0f,180f,0f);
            face_direction=!face_direction;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("player")&&other.GetType().ToString()=="UnityEngine.PolygonCollider2D")
        {
            if(player_controler.player_blood>0)
            {
                player.gethurt(damage); 
            }      
        }
        else if(other.gameObject.CompareTag("player")&&other.GetType().ToString()=="UnityEngine.BoxCollider2D")
        {
            player.jump();
            Destroy(gameObject);
        }
    }
}
