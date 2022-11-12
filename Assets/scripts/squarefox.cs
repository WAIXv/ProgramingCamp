using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;
public class squarefox : MonoBehaviour
{
    public GameObject foxcoll;
    public GameObject Player;
    public Rigidbody2D RB2;
    public int jumpforce;
    public Move move;
    // Start is called before the first frame update
    void Start()
    {
        foxcoll = GameObject.Find("Square");
        Player = foxcoll.transform.parent.gameObject;
        RB2=Player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Frog")
        {
            Destroy(collision.gameObject);
            RB2.velocity = Vector2.up * move.jumps;
            move.Scoreplus();
        }
    }*/
}
