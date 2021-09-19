using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class DisplayEquipment : MonoBehaviour
{
    [Header("Equipment: ")]
    public EquipmentObject equipment;

    [Header("Sprite Manager: ")]
    public EquipmentCategory[] categories;

    [Header("Equipment Slot: ")]
    public EquipmentSlotManager chestPlate;
    public EquipmentSlotManager weapon;
    public SpriteRenderer weaponSlot;

    public void EquipmentUpdate(EquipmentSlot slot)
    {
        switch(slot.SlotName)
        {
            case "Helemt":
                break;
            case "ChestPlate":
                ChestPlateSlotUpdate();
                break;
            case "MainWeapon":
                WeaponSlotUpdate();
                break;
            case "SubWeapon":
                break;
            case "Boots":
                break;
            case "Ring":
                break;
            case "Necklace":
                break;
        }
    }

    public void ChestPlateSlotUpdate()
    {
        chestPlate.slot = equipment.Container[2];
        chestPlate.slot.parent = this;

        if(chestPlate.slot.item.Id > -1)
        {
            categories[0].resolver.SetCategoryAndLabel("ChestPlate", chestPlate.slot.item.Name);
        }
        else
        {
            categories[0].resolver.SetCategoryAndLabel("ChestPlate", "Default");
        }

        chestPlate.SlotDisplayUpdate();
    }

    public void WeaponSlotUpdate()
    {
        weapon.slot = equipment.Container[0];
        weapon.slot.parent = this;
        if(weapon.slot.ItemObject != null)
        {
            weaponSlot.sprite = weapon.slot.ItemObject.itemDisplay;
        }
        else
        {
            weaponSlot.sprite = null;
        }
        weapon.SlotDisplayUpdate();
    }
}
