using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ThornsEffect", menuName = "StatusEffects/Effect/Thorns")]
public class Thorns : StatusBase
{
    public Effect StatusEff;
    public float baseDmgMulti, DamageDealt;
    public int buildUpStackMax, maxDmgStacks;
    void Awake()
    {
        StatusEff = Effect.thorns;
    }

    
}
