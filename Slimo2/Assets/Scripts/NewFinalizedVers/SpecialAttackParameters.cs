using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackParameters : MonoBehaviour
{
    public int m_SpAtkID = 0;
    public int m_UltAtkID = 0;
    private Animator anim = null;
    private bool spAtkPressed = false;
    public SkillDatabase skDatabase;
    public float[] skillCDs = new float[2];
    public float[] swapSkillCDs = new float[2];
    private bool startCD = false;
    [SerializeField]
    private bool[] canUseAtks = new bool[4];
    [SerializeField]
    private bool[] atkUsed = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GetSkillData();
        
    }

    void FixedUpdate()
    {
        if(Input.GetAxisRaw("SpAtk") != 0 && canUseAtks[0])
        {
            if(!spAtkPressed)
            {
                spAtkPressed = true;
                anim.SetTrigger("SpAtk");
            }
            anim.SetBool("IsAttacking", true);
            anim.SetBool("spAtkHeld", true);
            atkUsed[0] = true;
        }
        if(Input.GetAxisRaw("SpAtk") == 0)
        {
            anim.SetBool("spAtkHeld", false);
            if(spAtkPressed)
            {
                spAtkPressed = false;
            }
        }
        if(startCD == true)
        {
            if(atkUsed[0] == true)
            {
                StartCoroutine(CDRest(0, skillCDs[0]));
                startCD = false;
            }
        }
    }
    private IEnumerator CDRest(int whichCD, float cd)
    {
        switch(whichCD)
        {
            case 0:

                yield return new WaitForSeconds(cd);
                canUseAtks[0] = true;
                break;
            case 1:

                yield return new WaitForSeconds(cd);

                break;
            case 2:

                yield return new WaitForSeconds(cd);

                break;
            case 3:

                yield return new WaitForSeconds(cd);

                break;
        }
    }
    public void GetSkillData()
    {
        skillCDs[0] = skDatabase.returnCD(m_SpAtkID);
    }
    public void UpdateSkillIDs(int _spAtk, int _ultAtk)
    {
        m_SpAtkID = _spAtk;
        m_UltAtkID = _ultAtk;
        anim.SetInteger("spID", _spAtk);
        anim.SetInteger("ultID", _ultAtk);
    }
    public void PutSkillOnCD()
    {
        startCD = true;
    }
    public void DisableAtk(int _atkID)
    {
        if(m_SpAtkID == _atkID)
        {
            canUseAtks[0] = false;
        }
    }
}
