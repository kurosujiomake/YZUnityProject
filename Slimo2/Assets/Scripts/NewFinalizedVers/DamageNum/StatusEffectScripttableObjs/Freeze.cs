using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FreezeEffect", menuName = "StatusEffects/Effect/Freeze")]
public class Freeze : StatusBase
{
    public Effect StatusEff;
    public float Duration;
    void Awake()
    {
        StatusEff = Effect.freeze;
    }

    
}
