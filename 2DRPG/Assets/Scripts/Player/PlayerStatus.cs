using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public PlayerLevel level;
    public EquipmentObject equipment;
    public ReinforceSkillControl reinforceSkill;
    public Text hpBarText;
    public Image hpBar;
    public CharacterStat[] stats;
    public DisplayEquipment displayEquipment;

    public float currentHp;
    public float currentMP;
    public float attackRange;

    public float GetSTR { get { return STR.Value; } }
    public float GetAGI { get { return AGI.Value; } }
    public float GetDEX { get { return DEX.Value; } }
    public float GetVIT { get { return VIT.Value; } }
    public float GetINT { get { return INT.Value; } }
    public float GetLUK { get { return LUK.Value; } }
    public float GetHP { get { return HP.Value; } }
    public float GetMP { get { return MP.Value; } }
    public float GetATK { get { return ATK.Value; } }
    public float GetDEF { get { return DEF.Value; } }
    public float GetCRIT { get { return CRIT.Value; } }
    public float GetCRITDMG { get { return CRITDMG.Value; } }
    public float GetFLEE { get { return FLEE.Value; } }
    public float GetHIT { get { return HIT.Value; } }
    public float GetATKSPEED { get { return ATKSPEED.Value; } }
    
    private CharacterStat STR;
    private CharacterStat AGI;
    private CharacterStat DEX;
    private CharacterStat VIT;
    private CharacterStat INT;
    private CharacterStat LUK;
    private CharacterStat HP;
    private CharacterStat MP;
    private CharacterStat ATK;
    private CharacterStat DEF;
    private CharacterStat CRIT;
    private CharacterStat CRITDMG;
    private CharacterStat FLEE;
    private CharacterStat HIT;
    private CharacterStat ATKSPEED;

    void Start()
    {
        StatusManage();

        for (int i = 0; i < equipment.Container.Count; i++)
        {
            equipment.Container[i].OnBeforeUpdated += OnUnEquipItem;
            equipment.Container[i].OnAfterUpdated += OnEquipItem;
            OnEquipItem(equipment.Container[i]);
        }

        UpdateStatus();
        currentHp = HP.Value;
    }

    void StatusManage()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            switch(stats[i].type)
            {
                case Attributes.Strength:
                    STR = stats[i];
                    break;
                case Attributes.Agility:
                    AGI = stats[i];
                    break;
                case Attributes.Dexterity:
                    DEX = stats[i];
                    break;
                case Attributes.Vitality:
                    VIT = stats[i];
                    break;
                case Attributes.Intellect:
                    INT = stats[i];
                    break;
                case Attributes.Lucky:
                    LUK = stats[i];
                    break;
                case Attributes.MaxHealth:
                    HP = stats[i];
                    break;
                case Attributes.MaxMana:
                    MP = stats[i];
                    break;
                case Attributes.Attack:
                    ATK = stats[i];
                    break;
                case Attributes.Defense:
                    DEF = stats[i];
                    break;
                case Attributes.Crit:
                    CRIT = stats[i];
                    break;
                case Attributes.CritDMG:
                    CRITDMG = stats[i];
                    break;
                case Attributes.Flee:
                    FLEE = stats[i];
                    break;
                case Attributes.Hit:
                    HIT = stats[i];
                    break;
                case Attributes.AttackSpeed:
                    ATKSPEED = stats[i];
                    break;
            }
        }
    }

    public void OnUnEquipItem(EquipmentSlot slot)
    {
        if(slot.item.Id == -1) return;

        for (int i = 0; i < slot.item.buffs.Length; i++)
        {
            for (int j = 0; j < stats.Length; j++)
            {
                stats[j].RemoveAllModifiersFromSource(slot.item);
                stats[j].ModifiedValue = stats[j].Value;
            }

            if(slot.SlotName == "MainWeapon")
            {
                attackRange = 2;
            }
        }

        reinforceSkill.skill = null;

        UpdateStatus();
    }

    public void OnEquipItem(EquipmentSlot slot)
    {
        displayEquipment.EquipmentUpdate(slot);

        if(slot.item.Id == -1) return;

        for (int i = 0; i < slot.item.buffs.Length; i++)
        {
            for (int j = 0; j < stats.Length; j++)
            {
                if(slot.item.buffs[i].attribute == stats[j].type)
                {
                    stats[j].AddModifier(new StatModifier(slot.item.buffs[i].value, slot.item.buffs[i].modType, slot.item));
                    stats[j].ModifiedValue = stats[j].Value;
                }
            }

            if(slot.SlotName == "MainWeapon")
            {
                attackRange = slot.item.attackRange;
            }

        }

        if(slot.item.reinforceSkill != null)
        {
            reinforceSkill.skill = slot.item.reinforceSkill;
        }

        UpdateStatus();
    }

    public void OnSkillDurationStart(ReinforceSkill skill)
    {
        for (int i = 0; i < skill.attributes.Length; i++)
        {
            for (int j = 0; j < stats.Length; j++)
            {
                if(skill.attributes[i].type == stats[j].type)
                {
                    stats[j].AddModifier(new StatModifier(skill.attributes[i].value, skill.attributes[i].modType, skill));
                    stats[j].ModifiedValue = stats[j].Value;
                }
            }
        }

        UpdateStatus();
    }

    public void OnSkillDurationEnd(ReinforceSkill skill)
    {
        for (int i = 0; i < skill.attributes.Length; i++)
        {
            for (int j = 0; j < stats.Length; j++)
            {
                if(skill.attributes[i].type == stats[j].type)
                {
                    stats[j].RemoveAllModifiersFromSource(skill);
                    stats[j].ModifiedValue = stats[j].Value;
                }
            }
        }

        UpdateStatus();
    }

    void UpdateStatus()
    {
        HP.BaseValue = (VIT.Value * 10) + (level.level * 30);
        MP.BaseValue = (INT.Value * 4) + (level.level * 8);
        ATK.BaseValue = (STR.Value * 2) + (level.level * 5);
        DEF.BaseValue = (VIT.Value) + (level.level * 2);
        CRIT.BaseValue = LUK.Value / 2f;
        CRITDMG.BaseValue = LUK.Value / 4f;
        FLEE.BaseValue = Mathf.Round(AGI.Value / 1.5f);
        HIT.BaseValue = DEX.Value / 1.5f;
        ATKSPEED.BaseValue = AGI.Value / 2f;

        HP.ModifiedValue = HP.Value;
        MP.ModifiedValue = MP.Value;
        ATK.ModifiedValue = ATK.Value;
        DEF.ModifiedValue = DEF.Value;
        CRIT.ModifiedValue = CRIT.Value;
        FLEE.ModifiedValue = Mathf.Round(FLEE.Value);
        HIT.ModifiedValue = HIT.Value;
        ATKSPEED.ModifiedValue = ATKSPEED.Value;

        if(currentHp > HP.Value)
        {
            currentHp = HP.Value;
        }
    }
}
