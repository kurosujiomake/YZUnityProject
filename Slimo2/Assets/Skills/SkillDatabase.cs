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
        return Skills[ID].spawnsObj;
    }
    public GameObject returnSpawnObj(int ID)
    {
        return Skills[ID].spawn;
    }
    public float returnProjSpd(int ID)
    {
        return Skills[ID].projSpd;
    }
    public float returnProjDur(int ID)
    {
        return Skills[ID].projDur;
    }
}
