using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Armor Object", menuName = "Inventory System/Items/Armor")]
public class ArmorObject : ItemObject
{
    public float DefenseBonus;
    public int otherEffect1ID;
    public int otherEffect2ID;

    public void Awake()
    {
        type = ItemType.Armor;

    }
}
