using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Skill")]
public class SkillObject : ScriptableObject
{
    public Sprite uiDisplay;
    public SkillType type;
    [TextArea (15, 20)]
    public string description;
    public Skill data = new Skill();
}

[System.Serializable]
public class Skill
{
    public string Name;
    public int Id;
    public int damage;
}

