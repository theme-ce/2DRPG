using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Create New Enemy")]
public class EnemyObject : ScriptableObject
{
    public new string name;
    
    [Header("Status: ")]
    public float maxHp;
    public float attackDamage;
    public float attackRange;
    public float defense;
    public float dodge;

    [Header("Movement: ")]
    public float detectionRadius;
    public float moveSpeed;

    [Header("Drop: ")]
    public float expDrop;
    public int goldDrop;
    public ItemObject[] items;
}
