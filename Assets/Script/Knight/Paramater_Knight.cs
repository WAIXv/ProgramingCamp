using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Paramater_Knight
{
    public Transform trans;
    public Animator anim;
    public Rigidbody2D rb;
    public Collider2D coll;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject grabHand;
    public Collider2D attackHitBox;
    public LayerMask groundLayer;
    public float attackStartTime;
    public float attackHoldTime;
    public int hp;
    public float speed;
    public int jumpForce;
    public Dictionary<StateType, State> allSaveState;
    public StateType lastState;
    public StateType currentState;
    public bool spacePress;
    public bool attackPress;
    public bool isGround;
    public bool isOnWall;
}
