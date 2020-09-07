using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]
public class WeaponObject : ItemObject
{
    public enum WepClass
    {
        Dagger,
        Sword,
        Bow,
        Wand,
        Spear,
        Axe
    }
  
    public WepClass wepType;
    public float damageIncrease;
    public int[] skillID = new int[2];
    public void Awake()
    {
        type = ItemType.Weapon;

    }
}
