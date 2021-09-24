using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DamageSkill", menuName = "Skills/DamageSkill")]
public class DamageSkill : Skill
{
    public float baseDamage;
    public float damageMult;
}
