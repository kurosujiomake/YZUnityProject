using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMove : StateMachineBehaviour
{
    //private pMove move = null;
    private Parameters param = null;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //move = animator.GetComponent<pMove>();
        param = animator.GetComponent<Parameters>();
        //reset variables, this should only be called once whenever it gets into this state
        //animator.SetBool("IsIdle", true);
        animator.SetBool("IsMoving", false); //double make sure if player is idle then ismoving is false
        //animator.SetBool("InAir", false);
        animator.SetBool("IsDashing", false); //double make sure player is not dashing
        animator.GetComponent<pStates>().SetPState("idle");

    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //check for player stick inputs here to see which animation should the player move to
        if(animator.GetComponent<pStates>().ReturnGround()) //if player is grounded
        {
            if(Input.GetAxis("Horizontal") != 0) //if player inputs horizontal stick for movement
            {
                animator.SetBool("IsMoving", true);
                //animator.SetBool("IsIdle", false);
                //move.SetMoveState(0);
                animator.GetComponent<pStates>().SetPState("move");
            }
            if(Input.GetAxis("Jump") != 0 && !param.m_jumpPressed)
            {
                animator.SetTrigger("Jump");
            }
            
        }
        if(!animator.GetComponent<pStates>().ReturnGround()) //if player is not grounded
        {
            animator.GetComponent<pStates>().SetPState("jump");
        }
        animator.SetBool("IsDashing", param.m_isDashing);
        
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("IsIdle", false); //safety check to disable isIdle when animation leaves this state
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
