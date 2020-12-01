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
    private bool bowMelee = false;
    
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
        bowMelee = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isSwUlt) //code for non-sword ult atts go here
        {
            if (!numInc && isRegularAtk)
            {
                numInc = true;
                animator.SetInteger("ComboNum", animator.GetInteger("ComboNum") + 1);
            }
            switch (param.AT) //directional influenced triggers
            {
                case Parameters.AtkType.bow: //directional att triggers for bow
                    switch (pCM.GetDirectionL())
                    {
                        case "d":
                        case "dl":
                        case "dr":
                            //jumpback att goes here
                            break;
                        case "n":
                        case "l":
                        case "r":
                        case "u":
                        case "ul":
                        case "ur":
                            if (pCM.GetButtonDown("Atk1"))
                            {
                                animator.SetTrigger("GAtt_a");
                                if(animator.GetBool("IsNearEnemy"))
                                {
                                    bowMelee = true;
                                }
                            }
                            break;
                    }
                    break;
                case Parameters.AtkType.sword: //directional att triggers for sword
                    switch (pCM.GetDirectionL())
                    {
                        case "d":
                        case "dl":
                        case "dr":
                        case "n":
                            if (pCM.GetButtonDown("Atk1"))
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
                        case "u":
                        case "ul":
                        case "ur":
                            if (pCM.GetButtonDown("Atk1"))
                            {
                                animator.SetTrigger("SwKnockUp");
                            }
                            break;
                    }
                    break;
                case Parameters.AtkType.axe: //directional att triggers for axe
                    switch (pCM.GetDirectionL())
                    {
                        case "d":
                        case "dl":
                        case "dr":
                        case "n":
                        case "l":
                        case "r":
                        case "u":
                        case "ul":
                        case "ur":
                            break;
                    }
                    break;
                case Parameters.AtkType.spear: //directional att triggers for spear
                    switch (pCM.GetDirectionL())
                    {
                        case "d":
                        case "dl":
                        case "dr":
                        case "n":
                        case "l":
                        case "r":
                        case "u":
                        case "ul":
                        case "ur":
                            break;
                    }
                    break;
                case Parameters.AtkType.dagger: //directional att triggers for dagger
                    switch (pCM.GetDirectionL())
                    {
                        case "d":
                        case "dl":
                        case "dr":
                        case "n":
                        case "l":
                        case "r":
                        case "u":
                        case "ul":
                        case "ur":
                            break;
                    }
                    break;
                case Parameters.AtkType.wand: //directional att triggers for wand
                    switch (pCM.GetDirectionL())
                    {
                        case "d":
                        case "dl":
                        case "dr":
                        case "n":
                        case "l":
                        case "r":
                        case "u":
                        case "ul":
                        case "ur":
                            break;
                    }
                    break;
            }
            if (pCM.GetButtonDown("Atk2") && nSP.ReturnCanUse(0))
            {
                animator.SetTrigger("SpAtk");
            }
            if (pCM.GetButtonDown("Atk3") && nSP.ReturnCanUse(2))
            {
                animator.SetTrigger("SwordUlt");
                animator.ResetTrigger("GAtt_a");
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
        if(bowMelee)
        {
            animator.ResetTrigger("GAtt_a");
            bowMelee = false;
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
