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

}
