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
    public EquipmentSlotManager helmet;
    public EquipmentSlotManager chestPlate;
    public EquipmentSlotManager weapon;
    public SpriteRenderer weaponSlot;
    public EquipmentSlotManager boots;
    public EquipmentSlotManager ring;
    public EquipmentSlotManager necklace;

    public void EquipmentUpdate(EquipmentSlot slot)
    {
        switch(slot.SlotName)
        {
            case "Helmet":
                HelmetSlotUpdate();
                break;
            case "ChestPlate":
                ChestPlateSlotUpdate();
                break;
            case "MainWeapon":
                WeaponSlotUpdate();
                break;
            case "Boots":
                BootsSlotUpdate();
                break;
            case "Ring":
                RingSlotUpdate();
                break;
            case "Necklace":
                NecklaceSlotUpdate();
                break;
        }
    }

    public void ChestPlateSlotUpdate()
    {
        chestPlate.slot = equipment.Container[2];
        chestPlate.slot.parent = this;

        if(chestPlate.slot.item.Id > -1)
        {
            categories[0].resolver.SetCategoryAndLabel("Body", chestPlate.slot.item.Name);
        }
        else
        {
            categories[0].resolver.SetCategoryAndLabel("Body", "Default");
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

    public void HelmetSlotUpdate()
    {
        helmet.SlotDisplayUpdate();
    }

    public void BootsSlotUpdate()
    {
        boots.SlotDisplayUpdate();
    }

    public void RingSlotUpdate()
    {
        ring.SlotDisplayUpdate();
    }

    public void NecklaceSlotUpdate()
    {
        necklace.SlotDisplayUpdate();
    }
}
