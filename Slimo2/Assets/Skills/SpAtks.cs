﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Special Attacks")]
public class SpAtks : BaseSkills
{
    public WepClass wep;
    public bool useCDMod;
    public void Awake()
    {
        type = SkillType.Special;
        if(!useCDMod)
        {
            CDModifiers = new float[0];
        }
    }
}