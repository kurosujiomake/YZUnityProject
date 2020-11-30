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
        if(pCM.GetButtonDown("Atk3"))
        {
            if(nSP.ReturnCanUse(2))
            {
                animator.SetTrigger("SwordUlt");
            }
            
        }
        switch(pCM.GetDirectionL())
        {
            case "d":
            case "n":
                if (pCM.GetButtonDown("Atk1"))
                {
                     animator.SetTrigger("GAtt_a");
                }
                break;
            case "l":
            case "r":
                if(pCM.GetButtonDown("Atk1"))
                {
                    switch (param.AT)
                    {
                        case Parameters.AtkType.sword:
                            animator.SetTrigger("GDashAtk");
                            break;
                        case Parameters.AtkType.bow:

                            break;
                    }
                }
                break;
            case "u":
            case "ul":
            case "ur":
                if(pCM.GetButtonDown("Atk1"))
                {
                    switch(param.AT)
                    {
                        case Parameters.AtkType.sword:
                            animator.SetTrigger("SwKnockUp");
                            break;
                            //add other weapon types stuff later
                    }
                }
                break;
        }
        
        if(pCM.GetButtonDown("Atk2"))
        {
            if(nSP.ReturnCanUse(0))
            {
                animator.SetTrigger("SpAtk");
            }
        }

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
