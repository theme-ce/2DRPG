using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public EquipmentObject equipment;

    public Attribute[] attributes;
    public Attribute Strength => attributes[0];

    void Start()
    {
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        
        for (int i = 0; i < equipment.Container.Count; i++)
        {
            equipment.Container[i].OnBeforeUpdated += OnUnEquipItem;
            equipment.Container[i].OnAfterUpdated += OnEquipItem;
            OnEquipItem(equipment.Container[i]);
        }
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug
            .Log(string
                .Concat(attribute.type,
                " was updated! Value is now ",
                attribute.value.ModifiedValue));
    }

    public void OnUnEquipItem(EquipmentSlot slot)
    {
        if(slot.item == null) return;

        for (int i = 0; i < slot.item.buffs.Length; i++)
        {
            for (int j = 0; j < attributes.Length; j++)
            {
                if(slot.item.buffs[i].attribute == attributes[j].type)
                {
                    attributes[j].value.RemoveModifier(slot.item.buffs[i]);
                }
            }
        }
    }

    public void OnEquipItem(EquipmentSlot slot)
    {
        if(slot.item == null) return;

        for (int i = 0; i < slot.item.buffs.Length; i++)
        {
            for (int j = 0; j < attributes.Length; j++)
            {
                if(slot.item.buffs[i].attribute == attributes[j].type)
                {
                    attributes[j].value.AddModifier(slot.item.buffs[i]);
                }
            }
        }
    }
}
