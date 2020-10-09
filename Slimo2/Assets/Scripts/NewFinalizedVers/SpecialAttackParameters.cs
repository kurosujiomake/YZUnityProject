using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackParameters : MonoBehaviour
{
    public int m_SpAtkID = 0;
    public int m_UltAtkID = 0;
    private Animator anim = null;
    private bool spAtkPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(Input.GetAxisRaw("SpAtk") != 0)
        {
            if(!spAtkPressed)
            {
                spAtkPressed = true;
                anim.SetTrigger("SpAtk");
                anim.SetBool("InSpAtk", true);
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
    public void UpdateSkillIDs(int _spAtk, int _ultAtk)
    {
        m_SpAtkID = _spAtk;
        m_UltAtkID = _ultAtk;
        anim.SetInteger("spID", _spAtk);
        anim.SetInteger("ultID", _ultAtk);
    }
    public void Slashed()
    {

    }
}
