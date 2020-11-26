using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Ultimate Attacks")]
public class UltimateAttacks : BaseSkills
{
    public WepClass Weapon;
    public bool useCDMod;
    public bool spawnsProj;
    public int projID;
    public int projIDFinisher;
    public int a_projID;
    public int a_projIDFinisher;
    public void Awake()
    {
        type = SkillType.Ultimate;
        if (!useCDMod)
        {
            CDModifiers = new float[0];
        }
    }
}
