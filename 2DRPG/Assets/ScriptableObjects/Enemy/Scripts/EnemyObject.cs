using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Create New Enemy")]
public class EnemyObject : ScriptableObject
{
    public new string name;

    public float maxHp;
    public float attackDamage;
    public float attackRange;
    public float defense;
    public float dodge;

    public float detectionRadius;
}
