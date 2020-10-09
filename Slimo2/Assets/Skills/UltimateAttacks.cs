using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Ultimate Attacks")]
public class UltimateAttacks : BaseSkills
{
    public WepClass Weapon;
    public bool useCDMod;

    public void Awake()
    {
        type = SkillType.Ultimate;
        if (!useCDMod)
        {
            CDModifiers = new float[0];
        }
    }
}
