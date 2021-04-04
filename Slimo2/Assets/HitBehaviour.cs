using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBehaviour : StateMachineBehaviour
{
    public GroundChecker g;
    public Parameters param;
    public PlayerControllerNew pCN;
    private float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(g == null)
        {
            g = animator.GetComponent<GroundChecker>();
        }
        if(param == null)
        {
            param = animator.GetComponent<Parameters>();
        }
        if(pCN == null)
        {
            pCN = animator.GetComponent<PlayerControllerNew>();
        }
        timer = param.hitStunDuration;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("GAtt_a");
        animator.ResetTrigger("AAtt_a");
        animator.ResetTrigger("BowAtt");
        animator.ResetTrigger("SwKnockUp");
        switch(param.canAerialRecover)
        {
            case true:
                timer -= Time.deltaTime;
                break;
            case false:
                if(g.ReturnGroundCheck())
                {
                    timer -= Time.deltaTime;
                }
                break;
        }
        if(timer <= 0)
        {
            animator.SetTrigger("BackToIdle");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pCN.SetPState(1); //returns control to the player
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
