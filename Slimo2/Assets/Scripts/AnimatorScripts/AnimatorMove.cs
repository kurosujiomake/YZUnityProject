using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorMove : StateMachineBehaviour
{
    //private pMove move = null;
    private Parameters param = null;
    private GroundChecker g = null;
    public PlayerControlManager pCM = null;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        param = animator.GetComponent<Parameters>();
        g = animator.GetComponent<GroundChecker>();
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        //reset variables, this should only be called once whenever it gets into this state
        animator.SetBool("IsMoving", false); //double make sure if player is idle then ismoving is false
        animator.SetBool("IsDashing", false); //double make sure player is not dashing
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsDashing", param.m_isDashing);
        //animator.SetBool("OnGround", g.ReturnGroundCheck());
        //check for player stick inputs here to see which animation should the player move to
        if (g.ReturnGroundCheck()) //if player is grounded
        {
            //animator.SetBool("OnGround", true);
            if(pCM.ReturnAxis("left", "hori") != 0)
            {
                animator.SetBool("IsMoving", true);
            }
            if(pCM.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jump");
            }
            animator.SetInteger("AComboNum", 1);
        }
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
