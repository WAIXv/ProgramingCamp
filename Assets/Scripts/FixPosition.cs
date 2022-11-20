using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPosition : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider2D;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask Ground;
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        player = GetComponent<Transform>();  
        Ground = GetComponent<LayerMask>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.position = player.position;
        transform.localScale = player.localScale;

    }
    private void LateUpdate()
    {
        if (collider2D.IsTouchingLayers(Ground))
        {
            player.position += ((gameObject.transform.position.x - player.position.x) > 0 ? Vector3.left : Vector3.right);
        }
    }
}
