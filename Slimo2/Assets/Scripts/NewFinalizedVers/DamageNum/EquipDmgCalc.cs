using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipDmgCalc : MonoBehaviour
{
    public EquipStats[] equips;
    public float ReturnTotalBaseAtk() //returns the combined total base atk
    {
        float a = 0;
        foreach(EquipStats e in equips)
        {
            a += e.baseAtk;
        }
        return a;
    }
    public float ReturnTotalAtkMulti() //returns the combined total atk multi
    {
        float a = 0;
        foreach(EquipStats e in equips)
        {
            a += e.AtkMulti;
        }
        return a;
    }
    public float ReturnTotalEleMulti() //returns the combined total ele multi
    {
        float a = 0;
        foreach(EquipStats e in equips)
        {
            a += e.EleMulti;
        }
        return a;
    }
    public int ReturnEquipEleMod()
    {
        int e = 0;
        if(equips[1].EleMod != 0) //subweapon is lowest on piority
        {
            e = equips[1].EleMod;
        }
        if(equips[0].EleMod != 0) //weapon has higher piority than sub
        {
            e = equips[0].EleMod;
        }
        if (equips[2].EleMod != 0) //relic has highest piority in equips
        {
            e = equips[2].EleMod;
        }
        return e;
    }
    public float ReturnTotalBaseDef()
    {
        float d = 0;
        foreach(EquipStats e in equips)
        {
            d += e.baseDef;
        }
        return d;
    }
    public float ReturnTotalDefMulti()
    {
        float d = 0;
        foreach(EquipStats e in equips)
        {
            d += e.DefMulti;
        }
        return d;
    }
}
[System.Serializable]
public class EquipStats
{
    public string slotName;
    public int ID, EquipID;
    public float baseAtk, baseDef, baseCrit, critChanceMulti, critDmgMulti;
    public int EleMod;
    public float AtkMulti, DefMulti, EleMulti;
}
