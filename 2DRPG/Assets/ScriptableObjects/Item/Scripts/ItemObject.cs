using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public Sprite itemDisplay;
    public bool stackable;
    public ItemTypes type;
    [TextArea(15, 20)]
    public string description;
    public Item data = new Item();
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public float attackRange;
    public ItemBuff[] buffs;
    public DamageSkill damageSkill;
    public ReinforceSkill reinforceSkill;
    public MobilitySkill mobilitySkill;
    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        attackRange = item.data.attackRange;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                attribute = item.data.buffs[i].attribute,
                modType = item.data.buffs[i].modType
            };
        }
        damageSkill = item.data.damageSkill;
        reinforceSkill = item.data.reinforceSkill;
        mobilitySkill = item.data.mobilitySkill;
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public StatModType modType;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}