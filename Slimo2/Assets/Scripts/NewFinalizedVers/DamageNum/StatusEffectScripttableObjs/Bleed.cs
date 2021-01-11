using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BleedEffect", menuName = "StatusEffects/Effect/Bleed")]
public class Bleed : StatusBase
{
    public Effect StatusEff;
    public float Duration, Multiplier;
    void Awake()
    {
        StatusEff = Effect.bleed;
    }
    
}
