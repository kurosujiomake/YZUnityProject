using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboStart : StateMachineBehaviour
{
    public Parameters param = null;
    public PlayerControlManager pCM = null;
    public PlayerControllerNew pCN = null;
    public SpecialAttackParameters spParam = null;
    //public pMove move = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        param = animator.GetComponent<Parameters>();
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        pCN = animator.GetComponent<PlayerControllerNew>();
        spParam = animator.GetComponent<SpecialAttackParameters>();
        animator.SetInteger("ComboNum", 1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch(pCM.GetDirectionL())
        {
            case "n":
                if (pCM.GetButtonDown("Atk1"))
                {
                    switch (param.AT)
                    {
                        case Parameters.AtkType.sword:
                            animator.SetTrigger("GAtt_a");
                            pCN.SetPState(2);
                            break;
                        case Parameters.AtkType.bow:

                            break;
                            //add the other wep types later
                    }
                }
                break;
            case "l":
            case "r":
                switch(param.AT)
                {
                    case Parameters.AtkType.sword:

                        break;
                    case Parameters.AtkType.bow:

                        break;
                }
                break;
        }
        
        if(pCM.GetButtonDown("Atk2"))
        {
            if(spParam.SpAtks[0].CanUseAtk)
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
