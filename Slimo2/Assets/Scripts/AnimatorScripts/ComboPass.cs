using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboPass : StateMachineBehaviour
{
    private bool numInc = false;
    private PlayerControlManager pCM;
    private SpecialAttackParameters spParam;
    [SerializeField] private bool isFinalAtk = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        numInc = false;
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        spParam = animator.GetComponent<SpecialAttackParameters>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("OnGround", animator.GetComponent<GroundChecker>().ReturnGroundCheck());
        if (!numInc && !isFinalAtk)
        {
            numInc = true;
            animator.SetInteger("ComboNum", animator.GetInteger("ComboNum") + 1);
        }
        if(pCM.GetButtonDown("Atk2") && spParam.SpAtks[0].CanUseAtk)
        {
            animator.SetTrigger("SpAtk");
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
