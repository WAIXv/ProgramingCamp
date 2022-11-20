using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBat : Enemy
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private CircleCollider2D  SearchPlayer;
    [SerializeField] private LayerMask  PlayerLayer;
    [SerializeField] private float  FlyTime;
    // Start is called before the first frame update
    public enum BatState
    {
        Idle,FlyAway
    }
    public BatState state;
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case BatState.Idle:
                {
                    if(SearchPlayer.IsTouchingLayers(PlayerLayer))
                    {
                        state = BatState.FlyAway;
                    }
                    break;
                }

            case BatState.FlyAway:
                {
                    rigidbody.velocity = Vector2.up;
                    FlyTime += Time.fixedDeltaTime;
                    if(FlyTime >= 10)
                    {
                        FlyTime = 0;
                        state = BatState.Idle;
                    }
                    break;
                }

            default:
                break;
        }
    }
}
