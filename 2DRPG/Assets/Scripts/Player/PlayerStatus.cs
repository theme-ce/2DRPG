using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public EquipmentObject equipment;
    public CharacterStat[] stats;
    public DisplayEquipment displayEquipment;

    public int currentHp;
    public int maxHp;
    public int currentMp;
    public int maxMp;
    public int attackDamage;
    public int defense;
    public int dodge;
    public int crit;
    public float attackRange;

    void Start()
    {
        for (int i = 0; i < equipment.Container.Count; i++)
        {
            equipment.Container[i].OnBeforeUpdated += OnUnEquipItem;
            equipment.Container[i].OnAfterUpdated += OnEquipItem;
            OnEquipItem(equipment.Container[i]);
        }
    }

    public void OnUnEquipItem(EquipmentSlot slot)
    {
        if(slot.item.Id == -1) return;

        for (int i = 0; i < slot.item.buffs.Length; i++)
        {
            for (int k = 0; k < stats.Length; k++)
            {
                stats[k].ModifiedValue = stats[k].BaseValue;
                stats[k].RemoveAllModifiersFromSource(slot.item);
                stats[k].ModifiedValue = stats[k].Value;
            }

            if(slot.SlotName == "MainWeapon")
            {
                attackRange = 0;
            }
        }
    }

    public void OnEquipItem(EquipmentSlot slot)
    {
        displayEquipment.EquipmentUpdate(slot);

        if(slot.item.Id == -1) return;

        for (int i = 0; i < slot.item.buffs.Length; i++)
        {
            for (int k = 0; k < stats.Length; k++)
            {
                if(slot.item.buffs[i].attribute == stats[k].type)
                {
                    stats[k].AddModifier(new StatModifier(slot.item.buffs[i].value, slot.item.buffs[i].modType, slot.item));
                    stats[k].ModifiedValue = stats[k].Value;
                }
            }

            if(slot.SlotName == "MainWeapon")
            {
                attackRange = slot.item.attackRange;
            }
        }
    }
}
