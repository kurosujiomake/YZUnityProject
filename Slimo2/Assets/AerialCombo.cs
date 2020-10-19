using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialCombo : StateMachineBehaviour
{
    private PlayerControlManager pCM;
    private PlayerControllerNew pCN;
    private Parameters param;
    private PlayerAttackMove pAMove;
    [SerializeField] private bool isNotAtkState = true;
    private bool comNumSet = false;
    public int maxAirCombo = 3;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        comNumSet = false;
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        pCN = animator.GetComponent<PlayerControllerNew>();
        param = animator.GetComponent<Parameters>();
        pAMove = animator.GetComponent<PlayerAttackMove>();
        switch(isNotAtkState)
        {
            case true:
                pAMove.isAttacking = false;
                break;
            case false:
                pAMove.isAttacking = true;
                break;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch(isNotAtkState)
        {
            case true:
                pCN.SetPState(1);
                if(pCM.GetButtonDown("Atk1"))
                {
                    if(animator.GetInteger("AComboNum") <= maxAirCombo)
                    animator.SetTrigger("AAtt_a");
                }
                break;
            case false:
                if(pCM.GetButtonDown("Atk1"))
                {
                    if(!comNumSet)
                    {
                        animator.SetInteger("AComboNum", animator.GetInteger("AComboNum") + 1);
                    }
                    if(animator.GetInteger("AComboNum") < maxAirCombo - 1)
                    {
                        animator.SetTrigger("AAtt_a");
                    }
                }
                break;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        comNumSet = false;
        animator.ResetTrigger("GAtt_a");
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
