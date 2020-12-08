using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WepType
{
    Sword,
    Bow,
    Dagger,
    Spear,
    Axe,
    Wand,
    NoType //only used if this gets incorrectly asked for, such as on an armor item
}
[CreateAssetMenu(fileName = "Weapon", menuName = "Objects/Items/Weapon")]
public class WepObject : BaseObj
{
    public WepType wType;
    public int SpAtkID;
    public int UltAtkID;
    [SerializeField]
    private float BaseDmg;
    [SerializeField]
    private float DamageMultiplier;
    public float FinalDamage;
    void Awake()
    {
        ObjectType = ObjType.Weapon;
        DamageCalc();
    }

    public void DamageCalc()
    {
        FinalDamage = BaseDmg * DamageMultiplier;
    }
}
