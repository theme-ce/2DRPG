using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    public Item item;

    public GameObject itemInfo;

    public void OnClickItem()
    {
        var obj = Instantiate(itemInfo, Vector3.zero, Quaternion.identity, transform.parent.parent.parent.parent);

        obj.GetComponent<ItemInfo>().item = this.item;
        obj.GetComponent<ItemInfo>().fromInventory = true;
    }
}
