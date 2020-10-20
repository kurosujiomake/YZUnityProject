using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialAnimations : StateMachineBehaviour
{
    public Parameters m_param = null;
    private PlayerControlManager pCM = null;
    private GroundChecker g = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_param = animator.GetComponent<Parameters>();
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        g = animator.GetComponent<GroundChecker>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsADashing", m_param.m_isADashing);
        animator.SetBool("IsTPing", m_param.m_isTPing);
        //animator.SetBool("OnGround", g.ReturnGroundCheck());
        if (pCM.GetDirectionL() == "l" || pCM.GetDirectionL() == "r")
        {
            animator.SetInteger("H or V", 0);
        }
        if (pCM.GetDirectionL() == "u" || pCM.GetDirectionL() == "d")
        {
            animator.SetInteger("H or V", 1);
        }
        if(pCM.GetDirectionL() == "n" && m_param.m_canBTP && pCM.GetButtonDown("Dash"))
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
