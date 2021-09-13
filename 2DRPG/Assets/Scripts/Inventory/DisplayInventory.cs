using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public GameObject inventoryItem;

    Dictionary<InventorySlot, GameObject>
        itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            inventory.Container[i].parent = this;
        }
        
        inventory.OnUpdate += UpdateInventory;

        InventoryDisplayStart();
    }

    void InventoryDisplayStart()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj =
                Instantiate(inventoryItem,
                Vector3.zero,
                Quaternion.identity,
                transform);
            obj.transform.GetChild(0).GetComponent<Text>().text =
                inventory.Container[i].item.Name;

            obj.GetComponent<SelectItem>().item = inventory.Container[i].item;

            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }

    public void UpdateInventory()
    {
        if(itemsDisplayed.Count != inventory.Container.Count)
        {
            itemsDisplayed.Clear();

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (!itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                var obj =
                    Instantiate(inventoryItem,
                    Vector3.zero,
                    Quaternion.identity,
                    transform);
                obj.transform.GetChild(0).GetComponent<Text>().text =
                    inventory.Container[i].item.Name;

                obj.GetComponent<SelectItem>().item =
                    inventory.Container[i].item;

                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }
}
