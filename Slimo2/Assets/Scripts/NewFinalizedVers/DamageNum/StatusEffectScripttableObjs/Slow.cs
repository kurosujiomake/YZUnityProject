using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SlowEffect", menuName = "StatusEffects/Effect/Slow")]
public class Slow : StatusBase
{
    public Effect StatusEff;
    public float Duration;
    public float Maginitude;
    void Awake()
    {
        StatusEff = Effect.slow;
    }

}
