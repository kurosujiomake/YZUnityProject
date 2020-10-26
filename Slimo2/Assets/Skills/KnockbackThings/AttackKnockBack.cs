using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Based KnockBack", menuName = "KnockBack/Attack")]
public class AttackKnockBack : KBBase
{
    public void Awake()
    {
        type = KBType.Attack;
    }
}
