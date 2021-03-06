﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkills : ScriptableObject
{
    public enum WepClass
    {
        Sword,
        Bow,
        Spear,
        Axe,
        Wand
    }

    public enum SkillType
    {
        Special,
        Ultimate
    }
    public enum SpeedType
    {
        Slow,
        Fast
    }
    public string Name;
    public Sprite UIImage;
    public SkillType type;
    [TextArea(15, 20)]
    public string Description;
    public int SkillID;
    public float CD;
    public float[] CDModifiers;
    public float BaseDamage;
    public float DamageModifier;
    public bool canAerial;
}
