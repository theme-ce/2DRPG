using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Item item;
    public bool fromInventory;
    public Text weaponName;
    public Text weaponBuffs;
    public Text equipText;
    public InventoryObject inventory;
    public EquipmentObject equipment;

    void Start()
    {
        GetComponent<RectTransform>().localPosition = Vector3.zero;

        weaponName.text = item.Name;
        
        string buffsInfo = "Attrubutes" + System.Environment.NewLine;

        for (int i = 0; i < item.buffs.Length; i++)
        {
            buffsInfo += item.buffs[i].attribute + ": " + item.buffs[i].value + System.Environment.NewLine;
        }

        weaponBuffs.text = buffsInfo;

        equipText.text = fromInventory ? "Equip" : "UnEquip";
    }

    public void CloseItemInfo()
    {
        Destroy(this.gameObject);
    }

    public void DeleteItem()
    {
        if(fromInventory)
        {
            inventory.RemoveItem(item);
            Destroy(this.gameObject);
        }
        else
        {
            equipment.SlotForItem(item).RemoveItem();
            Destroy(this.gameObject);
        }
    }

    public void InteractItem()
    {
        if(fromInventory)
        {
            if(equipment.SlotForItem(item).item.Id > -1)
            {
                Item temp = equipment.SlotForItem(item).item;
                inventory.AddItem(temp);
            }

            equipment.EquipItem(item);
            inventory.RemoveItem(item);
            Destroy(this.gameObject);
        }
        else
        {
            inventory.AddItem(item);
            equipment.UnEquipItem(item);
            Destroy(this.gameObject);
        }
    }
}
