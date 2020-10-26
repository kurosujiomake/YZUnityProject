using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KBType
{
    Attack,
    Other
}
public class KBBase : ScriptableObject
{
    public int ID;
    [TextArea(10, 15)]
    public string Desciption;
    public float Dir;
    public float Velocity;
    public float KBDuration;
    public KBType type;
    public bool FloatsTarget;
    public float FloatDuration;
}
