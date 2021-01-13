using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBase : ScriptableObject
{ 
    public enum Effect
    {
        bleed,
        shock,
        slow,
        freeze,
        burn,
        thorns,
        poison
    }
    [TextArea(5,15)]
    public string Desc;

    public float ReturnDuration()
    {
        float a = 0;
        if(this is Shock)
        {
            Shock s = this as Shock;
            a = s.Duration;
        }
        if(this is Bleed)
        {
            Bleed b = this as Bleed;
            a = b.Duration;
        }
        if(this is Slow)
        {
            Slow w = this as Slow;
            a = w.Duration;
        }
        if(this is Freeze)
        {
            Freeze f = this as Freeze;
            a = f.Duration;
        }
        return a;
    }
    public float ReturnMagni()
    {
        float a = 0;
        if(this is Shock)
        {
            Shock s = this as Shock;
            a = s.Magnitude;
        }
        if(this is Slow)
        {
            Slow w = this as Slow;
            a = w.Maginitude;
        }
        return a;
    }
    public float ReturnMulti()
    {
        float a = 0;
        if(this is Bleed)
        {
            Bleed b = this as Bleed;
            a = b.Multiplier;
        }
        if(this is Poison)
        {
            Poison p = this as Poison;
            a = p.DmgMulti;
        }
        return a;
    }
    public int ReturnDmgStacks()
    {
        int a = 0;
        if(this is Poison)
        {
            Poison p = this as Poison;
            a = p.Stacks;
        }
        return a;
    }
    public float ReturnDamage()
    {
        float a = 0;
        if(this is Thorns)
        {
            Thorns t = this as Thorns;
            a = t.DamageDealt;
        }

        return a;
    }
}
    

