using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMovingState : StateMachineBehaviour
{
    private Parameters param = null;
    private PlayerConsolidatedControl pC = null;
    //private pMove move = null; depreciated
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        param = animator.GetComponent<Parameters>();
        //move = animator.GetComponent<pMove>();
        pC = animator.GetComponent<PlayerConsolidatedControl>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsDashing", param.m_isDashing);
        if(pC.ReturnCanMove()) //check if it accepts player input
        {
            if (animator.GetComponent<pStates>().ReturnGround()) //if player is grounded
            {
                if (Input.GetAxis("Horizontal") == 0) //if player stopped horizontal input
                {
                    if (Input.GetAxis(param.AttButtonName) == 0) //if player did not input attack
                    {
                        animator.SetBool("IsMoving", false);
                        //animator.SetBool("IsIdle", true);
                    }
                    
                }
                if(Input.GetAxisRaw("Jump") != 0 && !param.m_jumpPressed)
                {
                    animator.SetTrigger("Jump");
                }
                if (Input.GetAxis(param.AttButtonName) != 0) //if player inputted attack, directly transition into attack state
                {
                    animator.SetBool("IsAttacking", true);
                    animator.SetBool("IsMoving", false);
                    //move.SetMoveState(2);
                    animator.GetComponent<pStates>().SetPState("attack");
                    if (param.AT == Parameters.AtkType.sword)
                    {
                        //animator.SetTrigger(param.GroundAttTriggerName);
                    }
                    if (param.AT == Parameters.AtkType.bow)
                    {
                        animator.SetTrigger(param.BowAttTriggerName[param.wepTypeID]);
                    }

                }
                
            }
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
