using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGiver : MonoBehaviour
{
    public OffensiveStats statBloc;
    public int hitCount;
    public EquipDmgCalc equipStats;

    void Awake()
    {
        equipStats = GetComponentInParent<EquipDmgCalc>();
        
    }
    public void CallDamage(EnemyDamageTake which) //this is the function that the hitbox will call
    {
        which.TakeDamage(statBloc.OuputDmg(), hitCount, statBloc.eleMod, statBloc.isCrit); 
    }
    public void DmgPass(float baseDmg, float dmgMulti, float eleMulti, int eleType, float baseCrit, float critChance, float critMulti)
    {
        statBloc.baseDmg = baseDmg;
        statBloc.dmgMulti = dmgMulti;
    }
}
[System.Serializable]
public class OffensiveStats
{
    public float baseDmg, dmgMulti, dmgRangePercent;
    public int eleMod;
    public float eleMulti;
    public float baseCrit, critChanceInc, critMulti;
    public bool isCrit = false;

    public float CritCalc(float dmgInput)
    {
        isCrit = false; //resets crit
        float d = dmgInput;
        float c = baseCrit * (1 + critChanceInc); //gets crit chance
        float a = Random.Range(0, 100); //crit roller
        if(a <= c) //if the random number rolls a crit
        {
            isCrit = true;
            d *= (1 + critMulti); //dmg gets crit multied
        }
        return d;
    }

    private float DmgRangeAdjustment(int type)
    {
        float a = 0;
        switch(type)
        {
            case 0: //basic range mod, nothing special
                a = Random.Range(-(baseDmg * dmgRangePercent), (baseDmg * dmgRangePercent));
                break;
            case 1: //the ele multi gets incorporated in the range calc, making the dmg range huge
                a = Random.Range(-(baseDmg * dmgRangePercent * (1 + eleMulti)), (baseDmg * dmgRangePercent * (1 + eleMulti)));
                break;
            case 2: // the mod will always be negative, giving the player only the lower half of dmg rolls
                a = Random.Range(-(baseDmg * dmgRangePercent), 0);
                break;
            case 3: //fire, the random rolls will always roll the higher number, resulting in more consistent high hits
                a = Random.Range(0 , (baseDmg * dmgRangePercent));
                break;
            case 4: //earth, 
                a = Random.Range(-(baseDmg * dmgRangePercent), (baseDmg * dmgRangePercent));
                break;
            case 5: //arcane, has no dmg variation
                a = 0;
                break;
        }
        if (a + baseDmg < 0) //if the random dmg range goes below 0
            a = -baseDmg + 1; //this way when a is added to base dmg it will output 1
        return a;
    }
    public float OuputDmg()
    {
        float o = 0;
        switch(eleMod) //different elemental type conversions will calc dmg a bit differently
        {
            case 0: //neutral, no additional mods, just adjusted base times multi, may apply bleed in the future
                o = (baseDmg + DmgRangeAdjustment(0)) * dmgMulti;
                break;
            case 1: //wind, huge dmg range, potential for very high and very low rolls, applies shock on crit, which is a 15% dmg inc
                o = (baseDmg + DmgRangeAdjustment(1)) * dmgMulti * eleMulti;
                break;
            case 2: //water, will always hit on the lower end of damage rolls, applies a slow on hit, freeze on crit
                o = (baseDmg + DmgRangeAdjustment(2)) * dmgMulti * eleMulti;
                break;
            case 3: //fire, ele dmg multi is doubled, making consistent high hits, may apply burn in the future, whidh inc target move spd
                o = (baseDmg + DmgRangeAdjustment(3)) * dmgMulti * eleMulti;
                break;
            case 4: //earth, ele dmg multi is halved, however consecutive hits apply impale, which deals an additional hit of dmg when hit again
                o = (baseDmg + DmgRangeAdjustment(4)) * dmgMulti * (eleMulti * 0.5f);
                break;
            case 5: //arcane, which has no dmg variance
                o = baseDmg * dmgMulti * eleMulti;
                break;
        }
        if (o <= 0) //dmg cant be negative
            o = 0;
        o = CritCalc(o); //calculates crit
        return o;
    }
}
