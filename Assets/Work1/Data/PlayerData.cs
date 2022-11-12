using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("移动状态")]
    [Tooltip("移动速度")]
    public float moveSpeed = 10f;

    [Header("跳跃状态")]
    [Tooltip("最大跳跃次数")]
    public int maxJumpCount = 2;
    [Tooltip("跳跃力度")]
    public float jumpForce = 15f;
    [Tooltip("延迟跳跃时间")]
    public float extraTime = 0.1f;


    [Header("检测")]
    [Tooltip("地面检测距离")]
    public float groundCheckRadius = 0.3f;
    [Tooltip("地面检测图层")]
    public LayerMask groundCheckLayer;

    [Tooltip("墙面检测距离")]
    public Vector2 wallCheckRange = new Vector2(1, 1);
    [Tooltip("地面检测图层")]
    public LayerMask wallCheckLayer;
}

