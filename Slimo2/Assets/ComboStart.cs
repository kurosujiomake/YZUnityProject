using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboStart : StateMachineBehaviour
{
    public Parameters param = null;
    //public pMove move = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        param = animator.GetComponent<Parameters>();
        //move = animator.GetComponent<pMove>();
        //move.SetMoveState(0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetAxisRaw(param.AttButtonName) != 0)
        {
            animator.SetBool("IsAttacking", true);
            //move.SetMoveState(2);
            if(param.AT == Parameters.AtkType.sword)
            {
                animator.SetTrigger(param.GroundAttTriggerName[param.wepTypeID]);
            }
            if(param.AT == Parameters.AtkType.bow)
            {
                animator.SetTrigger(param.BowAttTriggerName[param.wepTypeID]);

                animator.SetInteger("ComboNum", 1);
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
