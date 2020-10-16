using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : StateMachineBehaviour
{
    private Parameters m_param = null;
    private GroundChecker g = null;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_param = animator.GetComponent<Parameters>();
        g = animator.GetComponent<GroundChecker>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsTPing", m_param.m_isTPing);
        animator.SetBool("IsDashing", m_param.m_isDashing);
        animator.SetBool("IsADashing", m_param.m_isADashing);
        animator.SetBool("OnGround", g.ReturnGroundCheck());
        if(m_param.m_isTPing)
        {
            animator.SetTrigger("Blink");
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
