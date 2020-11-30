using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboPass : StateMachineBehaviour
{
    private bool numInc = false;
    private PlayerControlManager pCM;
    private SpecialAttackParameters spParam;
    [SerializeField] private bool isFinalAtk = false;
    [SerializeField] private bool isRegularAtk = false;
    [SerializeField] private bool isSwUlt = false;
    [SerializeField] private bool toConti = false;
    private NewSpUltAktParam nSP = null;
    private Parameters param = null;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        numInc = false;
        nSP = animator.GetComponent<NewSpUltAktParam>();
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        spParam = animator.GetComponent<SpecialAttackParameters>();
        param = animator.GetComponent<Parameters>();
        animator.ResetTrigger("SwordUlt");
        toConti = false;
        animator.SetTrigger("SwordUltFinish");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isSwUlt) //code for non-sword ult atts go here
        {
            //animator.SetBool("OnGround", animator.GetComponent<GroundChecker>().ReturnGroundCheck());
            if (!numInc && !isFinalAtk)
            {
                numInc = true;
                animator.SetInteger("ComboNum", animator.GetInteger("ComboNum") + 1);
            }
            if (pCM.GetButtonDown("Atk2") && spParam.SpAtks[0].CanUseAtk)
            {
                animator.SetTrigger("SpAtk");
            }
            if(pCM.GetButtonDown("Atk3"))
            {
                if(nSP.ReturnCanUse(2))
                {
                    animator.SetTrigger("SwordUlt");
                    animator.ResetTrigger("GAtt_a");
                }
                
            }
            switch (pCM.GetDirectionL())
            {
                case "u":
                    if (isRegularAtk && pCM.GetButtonDown("Atk1"))
                    {
                        animator.SetTrigger("SwKnockUp");
                    }
                    break;
                case "d":
                    break;
                case "n":
                    if (isRegularAtk && pCM.GetButtonDown("Atk1"))
                    {
                        animator.SetTrigger("GAtt_a");
                    }
                    break;
                case "l":
                case "r":
                    if (pCM.GetButtonDown("Atk1"))
                    {
                        animator.SetTrigger("GDashAtk");
                    }
                    break;
            }
        }
        if(isSwUlt) // code for sword atks go here
        {
            if(pCM.GetButtonDown("Atk3"))
            {
                animator.SetTrigger("SwordUlt");
                animator.ResetTrigger("GAtt_a");
                animator.ResetTrigger("GDashAtk");
                toConti = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!toConti)
        {
            animator.ResetTrigger("SwordUlt");
            animator.SetTrigger("SwordUltFinish");
            if(isSwUlt)
            {
                nSP.PutSkillOnCD(2);
                nSP.StartCD(2);
            }
            toConti = false;
        }
        if(toConti)
        {
            animator.SetTrigger("SwordUlt");
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
