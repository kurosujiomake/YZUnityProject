using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : StateMachineBehaviour
{
    private Parameters m_param = null;
    private float m_timer = 0;
    public bool m_AirDash = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_param = animator.GetComponent<Parameters>();
        if(!m_AirDash)
        {
            m_timer = m_param.m_GDTime;
            if (!m_param.m_isDashing)
            {
                animator.SetBool("IsDashing", true);
            }
        }
        if(m_AirDash)
        {
            m_timer = m_param.m_ADTime;
            
        }
        
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsTPing", m_param.m_isTPing);
        if(!m_AirDash)
        {
            m_timer -= Time.deltaTime;
            animator.SetBool("IsDashing", m_param.m_isDashing);
            if (m_timer <= 0)
            {
                animator.SetBool("IsDashing", false);
            }
            
        }
        if(m_AirDash)
        {
            animator.SetInteger("H or V", m_param.HorV);
            m_timer -= Time.deltaTime;
            animator.SetBool("IsADashing", m_param.m_isADashing);
            if(m_timer <= 0)
            {
                animator.SetBool("IsADashing", false);
            }
            if(m_param.m_isTPing)
            {
                animator.SetTrigger("Blink");
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
