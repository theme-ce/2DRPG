using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforceSkillControl : MonoBehaviour
{
    public PlayerStatus player;
    public ReinforceSkill skill;
    public float cooldownCount = 0f;
    public float durationCount = 0f;
    public SkillState state;
    public Image skillDisplay;
    public Image skillBG;

    void Update()
    {
        if(skill == null) { return; }

        switch(state)
        {
            case SkillState.OnReady:
                break;
            case SkillState.OnUsed:
                GetComponent<Image>().fillAmount = 0;
                player.OnSkillDurationStart(skill);
                state = SkillState.OnDuration;
                break;
            case SkillState.OnDuration:
                if(skill.duration >= durationCount)
                {
                    durationCount += Time.deltaTime;
                    break;
                }
                durationCount = 0f;
                player.OnSkillDurationEnd(skill);
                state = SkillState.OnCooldown;
                break;
            case SkillState.OnCooldown:
                if(skill.cooldown >= cooldownCount)
                {
                    cooldownCount += Time.deltaTime;
                    GetComponent<Image>().fillAmount += 1 / skill.cooldown * Time.deltaTime;
                    break;
                }
                cooldownCount = 0f;
                state = SkillState.OnReady;
                break;
        }
    }

    public void Activate()
    {
        if(state == SkillState.OnReady)
        {
            state = SkillState.OnUsed;
        }
    }
}
