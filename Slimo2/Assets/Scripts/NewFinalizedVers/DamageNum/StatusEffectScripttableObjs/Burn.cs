using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BurnEffect", menuName = "StatusEffects/Effect/Burn")]
public class Burn : StatusBase
{
    public Effect StatusEff;
    public float Duration, Magnitude;
    void Awake()
    {
        StatusEff = Effect.burn;
    }
}
