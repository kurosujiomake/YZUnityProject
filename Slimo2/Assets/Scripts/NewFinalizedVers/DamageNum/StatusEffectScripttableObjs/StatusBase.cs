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
        return a;
    }
}
    

