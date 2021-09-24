using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillState
{
    OnReady,
    OnUsed,
    OnDuration,
    OnCooldown
}

public class Skill : ScriptableObject 
{
    public new string name;
    public Sprite uiDisplay;
    public float cooldown;
    [TextArea(15,20)]
    public string description;

    public virtual void Activate()
    {

    }
}
