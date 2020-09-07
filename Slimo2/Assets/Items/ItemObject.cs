using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Weapon,
    Armor,
    Accessory,
    Relic,
    Misc
}

public enum EleDmg
{
    Ice,
    Fire,
    Wind,
    Earth,
    Arcane
}
public enum StatBonus
{
    Str,
    Vit,
    Dex,
    Int,
    HP,
    MP
}

public abstract class ItemObject : ScriptableObject
{
    public Sprite UIImage;
    public ItemType type;
    [TextArea(15,20)]
    public string desc;
    public bool Stackable;
    public Item data = new Item();

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int ID = -1;
    public bool Stackable;
    public ItemBuff[] stBuffs;
    public Item()
    {
        Name = "";
        ID = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        ID = item.data.ID;
        Stackable = item.Stackable;
        stBuffs = new ItemBuff[item.data.stBuffs.Length];
        for(int i = 0; i < stBuffs.Length; i++)
        {
            stBuffs[i] = new ItemBuff(item.data.stBuffs[i].min, item.data.stBuffs[i].max)
            {
                statBonus = item.data.stBuffs[i].statBonus
            };
            
        }
    }
}

[System.Serializable]
public class ItemBuff : iModifiers
{
    public StatBonus statBonus;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }

    public void GenerateValue()
    {
        value = Random.Range(min, max);
    }
}