using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpUltAktParam : MonoBehaviour
{
    public SkillDatabase skDB;
    public SKillBundle[] sk;
    void Awake()
    {
        SetSkills(0, -1, 1);
    }
    public bool ReturnCanUse(int _skill)
    {
        bool b = false;
        switch(_skill)
        {
            case 0:
                b = sk[0].canUseSkill;
                break;
            case 1:
                b = sk[1].canUseSkill;
                break;
            case 2:
                b = sk[2].canUseSkill;
                break;
        }
        return b;
    }
    public void SetSkills(int _sp1, int _sp2, int _ult)
    {
        if(_sp1 != -1)
        {
            sk[0].canUseSkill = true;
            sk[0].name = skDB.returnName(_sp1);
            sk[0].CD = skDB.returnCD(_sp1);
            sk[0].ID = _sp1;
        }
        if(_sp2 != -1)
        {
            sk[1].canUseSkill = true;
            sk[1].name = skDB.returnName(_sp2);
            sk[1].CD = skDB.returnCD(_sp2);
            sk[1].ID = _sp2;
        }
        if(_ult != -1)
        {
            sk[2].canUseSkill = true;
            sk[2].name = skDB.returnName(_ult);
            sk[2].CD = skDB.returnCD(_ult);
            sk[2].ID = _ult;
        }
    }
    public void StartCD(int _Skill)
    {
        StartCoroutine(SkillCD(_Skill));
    }
    public void PutSkillOnCD(int _skill)
    {
        sk[_skill].canUseSkill = false;
    }
    IEnumerator SkillCD(int _Skill)
    {
        switch(_Skill)
        {
            case 0:
                yield return new WaitForSeconds(sk[0].CD);
                sk[0].canUseSkill = true;
                break;
            case 1:
                yield return new WaitForSeconds(sk[1].CD);
                sk[1].canUseSkill = true;
                break;
            case 2:
                yield return new WaitForSeconds(sk[2].CD);
                sk[2].canUseSkill = true;
                break;
        }
    }
    
}
[System.Serializable]
public class SKillBundle
{
    public string name;
    public int ID;
    public float CD;
    public bool canUseSkill = false;
}
