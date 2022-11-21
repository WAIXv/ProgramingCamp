using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Paramater_Goblin
{
    public Animator anim;
    public PolygonCollider2D attackHitBox;
    public BoxCollider2D chaseBound;
    public Rigidbody2D rb;
    public GameObject target;
    public GameObject hand;
    public StateType currentState;
    public StateType lastState;
    public int hp;
    public float attackStartTime;
    public float attackHoldTime;
    public float speed;
    public LayerMask playerLayer;
    public bool isPlayerIn;
    public bool isAttacked;
    public bool canAttack;
}
