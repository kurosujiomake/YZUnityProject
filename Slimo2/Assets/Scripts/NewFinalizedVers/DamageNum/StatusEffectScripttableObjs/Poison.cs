using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PoisonEffect", menuName = "StatusEffects/Effect/Poison")]
public class Poison : StatusBase
{
    public Effect StatusEff;
    public float DmgMulti, Duration;
    public int Stacks;
    void Awake()
    {
        StatusEff = Effect.poison;
    }
}
