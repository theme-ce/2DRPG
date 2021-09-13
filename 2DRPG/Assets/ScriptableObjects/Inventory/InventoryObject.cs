using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void InventoryUpdate();

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public InventoryUpdate OnUpdate;
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(Item _item)
    {
        Container.Add(new InventorySlot(_item));

        if(OnUpdate != null)
        {
            OnUpdate.Invoke();
        }
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container.RemoveAt(i);
            }
        }

        if(OnUpdate != null)
        {
            OnUpdate.Invoke();
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item = new Item();
    public DisplayInventory parent;

    public InventorySlot(Item _item)
    {
        ID = _item.Id;
        item = _item;
    }

    public ItemObject ItemObject
    {
        get
        {
            if(item.Id >= 0)
            {
                return parent.inventory.database.ItemObjects[item.Id];
            }
            return null;
        }
    }
}