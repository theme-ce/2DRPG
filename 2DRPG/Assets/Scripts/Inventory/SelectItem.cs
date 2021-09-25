using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    public Item item;
    private Transform parent;
    public GameObject itemInfo;

    void Awake()
    {
        parent = GameObject.Find("ItemInfoParent").transform;
    }

    public void OnClickItem()
    {
        if(parent.childCount > 0) { Destroy(parent.GetChild(0).gameObject); }

        var obj = Instantiate(itemInfo, Vector3.zero, Quaternion.identity, parent);

        obj.GetComponent<ItemInfo>().item = this.item;
        obj.GetComponent<ItemInfo>().fromInventory = true;
    }
}
