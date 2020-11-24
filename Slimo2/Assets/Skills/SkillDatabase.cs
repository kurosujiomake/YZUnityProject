using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillsDatabase", menuName = "SkillDatabase")]
public class SkillDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public BaseSkills[] Skills;

    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            if (Skills[i].SkillID != i)
            {
                Skills[i].SkillID = i;
            }
        }
    }

    public void OnBeforeSerialize()
    {

    }
    public float returnCD(int ID)
    {
        return Skills[ID].CD;
    }
    public string returnName(int ID)
    {
        return Skills[ID].Name;
    }
    
    public bool returnHasProj(int ID)
    {
        bool b = false;
        if(Skills[ID] is SpAtks)
        {
            SpAtks s = Skills[ID] as SpAtks;
            b = s.spawnsProj;
        }
        if(Skills[ID] is UltimateAttacks)
        {
            UltimateAttacks u = Skills[ID] as UltimateAttacks;
            b = u.spawnsProj;
        }
        return b;
    }
    public int returnProjID(int ID)
    {
        int i = -1;
        if (Skills[ID] is SpAtks)
        {
            SpAtks s = Skills[ID] as SpAtks;
            i = s.projID;
        }
        if (Skills[ID] is UltimateAttacks)
        {
            UltimateAttacks u = Skills[ID] as UltimateAttacks;
            i = u.projID;
        }
        return i;
    }
    public int returnProjFinisherID(int ID)
    {
        int i = -1;
        if (Skills[ID] is SpAtks)
        {
            SpAtks s = Skills[ID] as SpAtks;
            i = s.projIDFinisher;
        }
        if (Skills[ID] is UltimateAttacks)
        {
            UltimateAttacks u = Skills[ID] as UltimateAttacks;
            i = u.projIDFinisher;
        }
        return i;
    }
}
