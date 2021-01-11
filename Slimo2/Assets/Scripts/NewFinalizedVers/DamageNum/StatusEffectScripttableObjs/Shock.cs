using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShockEffect", menuName = "StatusEffects/Effect/Shock")]
public class Shock : StatusBase
{
    public Effect StatusEff;
    public float Duration, Magnitude;
    void Awake()
    {
        StatusEff = Effect.shock;
    }

}
