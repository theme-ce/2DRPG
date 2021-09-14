using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory System/Equipment")]
public class EquipmentObject : ScriptableObject
{
    public string savePath;

    public ItemDatabaseObject database;

    public List<EquipmentSlot> Container = new List<EquipmentSlot>(4);

    public void EquipItem(Item item)
    {
        SlotForItem(item).UpdateSlot(item);
    }

    public EquipmentSlot FindItemSlot(Item item)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == item)
            {
                return Container[i];
            }
        }

        return null;
    }

    public EquipmentSlot SlotForItem(Item item)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].CanPlaceInSlot(database.ItemObjects[item.Id]))
            {
                return Container[i];
            }
        }
        return null;
    }
}

public delegate void SlotUpdated(EquipmentSlot _slot);

[System.Serializable]
public class EquipmentSlot
{
    public ItemTypes[] AllowedItems = new ItemTypes[0];

    public string SlotName;

    [System.NonSerialized]
    public DisplayEquipment parent;

    [System.NonSerialized]
    public GameObject slotDisplay;

    public SlotUpdated OnBeforeUpdated;

    public SlotUpdated OnAfterUpdated;

    public Item item;

    public ItemObject ItemObject
    {
        get
        {
            if (item.Id >= 0)
            {
                return parent.equipment.database.ItemObjects[item.Id];
            }
            return null;
        }
    }

    public void UpdateSlot(Item _item)
    {
        if(OnBeforeUpdated != null)
        {
            OnBeforeUpdated.Invoke(this);
        }

        item = _item;

        if(OnAfterUpdated != null)
        {
            OnAfterUpdated.Invoke(this);
        }
    }

    public void RemoveItem()
    {
        UpdateSlot(new Item());
    }

    public bool CanPlaceInSlot(ItemObject itemObject)
    {
        if (AllowedItems.Length <= 0 || itemObject == null || itemObject.data.Id < 0)
            return true;
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (itemObject.type == AllowedItems[i])
                return true;
        }
        return false;
    }
}
