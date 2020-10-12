using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpecialAttackParameters : MonoBehaviour
{
    public int CurrentSpAtk = 0;
    private Animator anim = null;
    private bool spAtkPressed = false;
    public SkillDatabase skDatabase;
    private bool startCD = false;
    public SkillsContainer[] skill = new SkillsContainer[3];
    public SkillsContainer spAtk;
    public SkillsContainer ultAtk;
    
    private bool cdStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //GetSkillData();
        SkillsUpdate();
        SetCurrentSkill(CurrentSpAtk);
        UpdateSkillIDs(spAtk.skillID, ultAtk.skillID);
    }

    void FixedUpdate()
    {
        SetCDBools();
        if (startCD == true)
        {
            StartCoroutine(CDRest(spAtk.skCD, spAtk.skillID));
            cdStarted = true;
        }
        if (Input.GetAxisRaw("SpAtk") != 0 && spAtk.CanUseAtk)
        {
            if(!spAtkPressed)
            {
                spAtkPressed = true;
                anim.SetTrigger("SpAtk");
            }
            anim.SetBool("IsAttacking", true);
            anim.SetBool("spAtkHeld", true);
        }
        if(Input.GetAxisRaw("SpAtk") == 0)
        {
            anim.SetBool("spAtkHeld", false);
            if(spAtkPressed)
            {
                spAtkPressed = false;
            }
        }
        
    }
    public void SetCDBools()
    {
        spAtk.CanUseAtk = skill[CurrentSpAtk].CanUseAtk;
    }

    public void SkillsUpdate()
    {
        //do this part later when equipment is set
        skill[0].skCD = skDatabase.returnCD(0);
        skill[0].SkillName = skDatabase.returnName(0);
        skill[0].skillID = skDatabase.Skills[0].SkillID;
        for(int i = 0; i < skill.Length; i++)
        {
            if(skill[i].SkillName != null)
            {
                skill[i].CanUseAtk = true;
            }
        }
    }
    public void SetCurrentSkill(int wepSet)
    {
        switch(wepSet)
        {
            case 0:
                spAtk = skill[0];
                //set ult here too
                break;
            case 1:
                //set here for weapon swap
                break;
        }
    }
    private IEnumerator CDRest(float _cd, int _skillID)
    {
        startCD = false;
        
        yield return new WaitForSeconds(_cd);
        for(int i = 0; i < skill.Length; i++)
        {
            if(skill[i].skillID == _skillID)
            {
                skill[i].CanUseAtk = true;
            }
        }
        cdStarted = false;
    }
    public void GetSkillData()
    {
        //
    }
    public void UpdateSkillIDs(int _spAtk, int _ultAtk)
    {
        anim.SetInteger("spID", _spAtk);
        anim.SetInteger("ultID", _ultAtk);
    }
    public void PutSkillOnCD()
    {
        if(!cdStarted)
        startCD = true;
    }
    public void DisableSPAtk()
    {
        for(int i = 0; i < skill.Length; i++)
        {
            if(skill[i].skillID == spAtk.skillID)
            {
                skill[i].CanUseAtk = false;
            }
        }
    }
}

[System.Serializable]
public class SkillsContainer
{
    public string SkillName;
    public int skillID;
    public float skCD;
    public bool CanUseAtk;
    

    
}