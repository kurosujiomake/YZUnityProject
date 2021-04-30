using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KUBehavior : StateMachineBehaviour
{
    public float keyHeldDur = 0.1f;
    private float timer = 0;
    private InputSystemShell pCM = null;
    private AtkMoveTransferScript atkT = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<InputSystemShell>();
        timer = 0;
        atkT = animator.GetComponentInChildren<AtkMoveTransferScript>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("SwKnockUp");
        if(pCM.GetButtonHeld("Atk1"))
        {
            timer += Time.deltaTime;
            if(timer >= keyHeldDur)
            {
                atkT.canfollowUpMove = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        atkT.canfollowUpMove = false;
        timer = 0;
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
