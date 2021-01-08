using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboStart : StateMachineBehaviour
{
    public Parameters param = null;
    public PlayerControlManager pCM = null;
    public PlayerControllerNew pCN = null;
    public SpecialAttackParameters spParam = null;
    public NewSpUltAktParam nSP = null;
    //public pMove move = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        param = animator.GetComponent<Parameters>();
        nSP = animator.GetComponent<NewSpUltAktParam>();
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        pCN = animator.GetComponent<PlayerControllerNew>();
        spParam = animator.GetComponent<SpecialAttackParameters>();
        animator.SetInteger("ComboNum", 1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch(param.AT)
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

        //non-directional att triggers go below (mostly special and ult atts)
        if(pCM.GetButtonDown("Atk3")) //checking if they can use ult
        {
            if(nSP.ReturnCanUse(2))
            {
                animator.SetTrigger("SwordUlt");
            }
            
        }
        if(pCM.GetButtonDown("Atk2")) //checking if they can use sp atk 1
        {
            if(nSP.ReturnCanUse(0))
            {
                animator.SetTrigger("SpAtk");
            }
        }
        if(pCM.GetButtonDown("Atk2_2")) //checking if they can use sp atk 2
        {
            if(nSP.ReturnCanUse(1))
            {
                animator.SetTrigger("SpAtk2");
            }
        }
        pCN.SetPState(1); //players should have free movement in this state

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
