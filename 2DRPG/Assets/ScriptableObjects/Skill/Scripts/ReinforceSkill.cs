using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ReinforceSkill", menuName = "Skills/ReinforceSkill")]
public class ReinforceSkill : Skill
{
    public float duration;
    public ReinforceAttribute[] attributes;
}

[System.Serializable]
public class ReinforceAttribute
{
    public Attributes type;
    public float value;
    public StatModType modType;
}
