using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Item item;
    public Text weaponName;
    public Text weaponBuffs;
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
    }

    public void CloseItemInfo()
    {
        Destroy(this.gameObject);
    }

    public void DeleteItem()
    {
        inventory.RemoveItem(item);
        
        Destroy(this.gameObject);
    }

    public void EquipItem()
    {
        equipment.EquipItem(item);

        inventory.RemoveItem(item);

        Destroy(this.gameObject);
    }
}
