using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotManager : MonoBehaviour
{
    public EquipmentSlot slot;
    public Image uiItem;
    public ItemObject itemObject;
    public GameObject itemInfo;
    public GameObject button;
    public Transform parent;

    public void SlotDisplayUpdate()
    {
        if(slot.item.Id > -1)
        {
            button.GetComponent<Button>().interactable = true;
            itemObject = slot.ItemObject;
            uiItem.sprite = itemObject.uiDisplay;
            uiItem.preserveAspect = true;
            Color color = new Color(1, 1, 1, 1);
            uiItem.color = color;
        }
        else if (slot.item.Id == -1)
        {
            button.GetComponent<Button>().interactable = false;
            itemObject = null;
            uiItem.sprite = null;
            Color color = new Color(1, 1, 1, 0);
            uiItem.color = color;
        }
    }

    public void OnClickEquipment()
    {
        if(parent.childCount > 0) { Destroy(parent.GetChild(0).gameObject); }

        var obj = Instantiate(itemInfo, Vector3.zero, Quaternion.identity, parent);

        obj.GetComponent<ItemInfo>().item = this.slot.item;
        obj.GetComponent<ItemInfo>().fromInventory = false;
    }
}
