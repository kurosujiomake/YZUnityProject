using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpAtk : StateMachineBehaviour
{
    [SerializeField]
    private int maxSlashes = 5;
    public int skillID = 0;
    private PlayerControlManager pCM;
    private SpecialAttackParameters spParam;
    public int whichAtk = 0;
    public string AtkButton = "Atk2";

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAttacking", true);
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        spParam = animator.GetComponent<SpecialAttackParameters>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetInteger("swSp1Counter") >= maxSlashes - 1)
        {
            spParam.SpAtks[whichAtk].CanUseAtk = false;
            spParam.StartCD(whichAtk);
            Debug.Log("Spatk1 skill CD called");
        }
        else
        {
            if(pCM.GetButtonDown("Atk2") && spParam.SpAtks[whichAtk].CanUseAtk)
            {
                animator.SetTrigger("SpAtk");
            }
        }
    }
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAttacking", false);
        if(animator.GetInteger("swSp1Counter") < maxSlashes)
        {
            animator.SetInteger("swSp1Counter", animator.GetInteger("swSp1Counter") + 1);
        }
        if(animator.GetInteger("swSp1Counter") >= maxSlashes)
        {
            animator.SetInteger("swSp1Counter", 1);
            //animator.GetComponent<SpecialAttackParameters>().DisableSPAtk();
        }
        
        
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
