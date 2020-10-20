using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class PostAttIdle : StateMachineBehaviour
{
    public Parameters param = null; //gets info from parameter script so you dont have to set every single one here
    public float timer = 0;
    private PlayerControlManager pCM = null;
    private PlayerControllerNew pCN = null;
    private GroundChecker g = null;
    [SerializeField] private bool isFinalPATT = false;
    [SerializeField] private bool attPressed = false;
    private SpecialAttackParameters spParam;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pCN = animator.GetComponent<PlayerControllerNew>();
        spParam = animator.GetComponent<SpecialAttackParameters>();
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        g = animator.GetComponent<GroundChecker>();
        //animator.SetBool("IsAttacking", false);
        param = animator.GetComponent<Parameters>();
        switch (isFinalPATT)
        {
            case true:
                timer = param.finalAtkDelayTimer;
                break;
            case false:
                timer = param.afterAtkDelayTimer;
                pCN.SetPState(1);
                break;
        }
        attPressed = false;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("OnGround", g.ReturnGroundCheck());
        if(timer > 0)
            timer -= Time.deltaTime;
        if (pCM.GetButtonDown("Atk2") && spParam.SpAtks[0].CanUseAtk && animator.GetInteger("ComboNum") >= 4)
        {
            animator.SetTrigger("SpAtk");
        }
        switch (isFinalPATT)
        {
            case true:
                animator.ResetTrigger("SpAtk");
                animator.ResetTrigger("GAtt_a");
                animator.SetInteger("ComboNum", 1);
                if (timer <= 0)
                {
                    animator.SetTrigger("BackToIdle");
                    pCN.SetPState(1);
                    if (pCM.ReturnAxis("left", "hori") != 0)
                        animator.SetBool("IsMoving", true);
                }
                //animator.GetComponent<SpecialAttackParameters>().PutSkillOnCD();
                    break;
            case false:
                if (pCM.ReturnAxis("left", "hori") != 0)
                    animator.SetBool("IsMoving", true);
                if (timer <= 0)
                {
                    animator.SetTrigger("BackToIdle");
                    animator.SetInteger("ComboNum", 1);
                    pCN.SetPState(1); //if somehow it skipped enter
                }
                if (pCM.GetButtonDown("Atk1") && g.ReturnGroundCheck()) //if there are still combos player can immediately go to the next att
                {
                    if(!attPressed)
                    {
                        attPressed = true;
                    }
                    switch(param.AT)
                    {
                        case Parameters.AtkType.sword:
                            if(g.ReturnGroundCheck())
                            {
                                animator.SetTrigger("GAtt_a");
                            }
                            break;
                        case Parameters.AtkType.bow:
                           
                            break;
                    }
                }
                
                break;
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("BackToIdle");
        animator.ResetTrigger("SpAtk");
        attPressed = false;
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
