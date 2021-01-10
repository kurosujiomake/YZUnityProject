using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class OffensiveStats
{
    public float baseDmg, dmgMulti, dmgRangePercent;
    public int eleMod;
    public float eleMulti;

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

                break;
            case 3: //fire, ele dmg multi is doubled, making consistent high hits

                break;
            case 4:

                break;
            case 5:

                break;
        }
        if (a + baseDmg <= 0) //if the random dmg range goes below 0
            a = -baseDmg + 1; //this way when a is added to base dmg it will output 1
        return a;
    }
    public float OuputDmg()
    {
        float o = 0;
        switch(eleMod) //different elemental type conversions will calc dmg a bit differently
        {
            case 0: //neutral, no additional mods, just adjusted base times multi
                
                break;
            case 1: //wind, huge dmg range, potential for very high and very low rolls

                break;
            case 2: //water, will always hit on the lower end of damage rolls

                break;
            case 3: //fire, ele dmg multi is doubled, making consistent high hits

                break;
            case 4:

                break;
            case 5:

                break;
        }
        if (o <= 0) //dmg cant be negative
            o = 0;
        return o;
    }
}
