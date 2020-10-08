using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
public class BaseSkills : ScriptableObject
{
    public Sprite UIImage;
    public SkillType type;
    [TextArea(15, 20)]
    public string Description;
    public int SkillID;
    public float CD;
    public float[] CDModifiers;
}
