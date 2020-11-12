using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

public class SpecialAttackParameters : MonoBehaviour
{
    public SkillsContainer[] SpAtks = new SkillsContainer[2];
    public SkillsContainer UltAtk;
    public SkillDatabase Data;


    void Start()
    {
        UpdateSkillIDS(0, -1, -1);
        EnableSkill(0);
    }
    public void GetSpATKData(int _spATK1ID, int _spATK2ID)
    {
        var skDB = Data.Skills;
        if (_spATK1ID != -1)
        {
            for (int i = 0; i < skDB.Length; i++)
            {
                if (skDB[i].SkillID == _spATK1ID)
                {
                    SpAtks[0].CD = Data.returnCD(_spATK1ID);
                    SpAtks[0].SkillName = Data.returnName(_spATK1ID);
                }
            }
        }
        if(_spATK2ID != -1)
        {
            for (int i = 0; i < skDB.Length; i++)
            {
                if (skDB[i].SkillID == _spATK2ID)
                {
                    SpAtks[1].CD = Data.returnCD(_spATK2ID);
                    SpAtks[1].SkillName = Data.returnName(_spATK2ID);
                }
            }
        }
        
    }

    public void UpdateSkillIDS(int _sk1, int _sk2, int _ult)
    {
        GetSpATKData(_sk1, _sk2);
        //Add ult data push here later
    }
    public void EnableSkill(int whichSlot)
    {
        SpAtks[whichSlot].CanUseAtk = true;
    }
    //Animator scripts cannot start coroutines directly, we input it here
    //0 means special atk 1, 1 means special atk 2, and 3 means ultimate
    public void StartCD(int whichAtk) 
    {
        StartCoroutine(SkillsCD(whichAtk)); 
    }
    IEnumerator SkillsCD(int whichSkill)
    {
        switch(whichSkill)
        {
            case 0:
                SpAtks[0].CanUseAtk = false;
                Debug.Log("CD called");
                yield return new WaitForSeconds(SpAtks[0].CD);
                Debug.Log("CD complete");
                SpAtks[0].CanUseAtk = true;
                break;
            case 1:
                SpAtks[1].CanUseAtk = false;
                yield return new WaitForSeconds(SpAtks[1].CD);
                SpAtks[1].CanUseAtk = true;
                break;
            case 2:
                UltAtk.CanUseAtk = false;
                yield return new WaitForSeconds(UltAtk.CD);
                UltAtk.CanUseAtk = true;
                break;
        }
    }
}

[System.Serializable]
public class SkillsContainer
{
    public string SkillName;
    public int skillID;
    public float CD;
    public bool CanUseAtk;
}