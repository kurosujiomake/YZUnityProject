using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMovingState : StateMachineBehaviour
{
    private Parameters param = null;
    private PlayerControlManager pCM = null;
    private GroundChecker g = null;
    //private pMove move = null; depreciated
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        param = animator.GetComponent<Parameters>();
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        g = animator.GetComponent<GroundChecker>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsDashing", param.m_isDashing);
        //animator.SetBool("OnGround", g.ReturnGroundCheck());
        if(g.ReturnGroundCheck())
        {
            if(pCM.ReturnAxis("left", "hori") == 0)
            {
                animator.SetBool("IsMoving", false);
            }
            if(pCM.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jump");
            }
            animator.SetInteger("AComboNum", 1);
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsMoving", false);
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
