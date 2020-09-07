using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Accessory Object", menuName = "Inventory System/Items/Accessory")]
public class AccessoryObject : ItemObject
{

    public EleBonusBundle[] eleBonus;
    public void Awake()
    {
        type = ItemType.Accessory;

    }
}

[System.Serializable]
public class EleBonusBundle
{
    public EleDmg eleType;
    public float eleBonusAmount;
}
