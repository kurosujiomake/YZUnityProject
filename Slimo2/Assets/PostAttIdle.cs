using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostAttIdle : StateMachineBehaviour
{
    public Parameters param = null; //gets info from parameter script so you dont have to set every single one here
    public float timer = 0;
    //public pMove move = null;
    public pStates pState = null;
    [SerializeField]
    private bool isFinalPATT = false;
    [SerializeField]
    private bool attPressed = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //move = animator.GetComponent<pMove>();
        pState = animator.GetComponent<pStates>();
        //move.SetMoveState(0);
        animator.SetBool("IsAttacking", false);
        param = animator.GetComponent<Parameters>();
        timer = param.delayTimer;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch(isFinalPATT)
        {
            case true:
                timer = 0.05f;
                animator.SetInteger("ComboNum", 1);
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                if (timer <= 0)
                {
                    animator.SetTrigger("BackToIdle");
                    
                }
                if (pState.ReturnGround())
                {
                    animator.SetBool("OnGround", true);
                    if (Input.GetAxis("Jump") != 0 && !param.m_jumpPressed)
                    {
                        animator.SetTrigger("Jump");
                    }
                    if (Input.GetAxis("Horizontal") != 0)
                    {
                        animator.SetBool("IsMoving", true);
                    }
                }
                    break;
            case false:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                if (timer <= 0)
                {
                    animator.SetTrigger("BackToIdle");
                    animator.SetInteger("ComboNum", 1);
                }
                if (Input.GetAxis(param.AttButtonName) > 0.2f && !attPressed) //if there are still combos player can immediately go to the next att
                {
                    attPressed = true;
                    switch(param.AT)
                    {
                        case Parameters.AtkType.sword:
                            if (pState.ReturnGround()) // ground sword attacks
                            {
                                animator.SetBool("IsAttacking", true);
                                animator.SetTrigger(param.GroundAttTriggerName[param.wepTypeID]);
                                animator.SetInteger("ComboNum", animator.GetInteger("ComboNum") + 1); //adds 1 to the cur combo count
                            }
                            if (!pState.ReturnGround()) //aerial sword attacks
                            {

                            }
                            break;
                        case Parameters.AtkType.bow:
                            if (pState.ReturnGround()) // ground bow attacks
                            {
                                animator.SetBool("IsAttacking", true);
                                animator.SetTrigger(param.BowAttTriggerName[param.wepTypeID]);
                                animator.SetInteger("ComboNum", animator.GetInteger("ComboNum") + 1); //adds 1 to the cur combo count
                            }
                            if (!pState.ReturnGround()) //aerial bow attacks
                            {

                            }
                            break;
                    }
                    //move.SetMoveState(2);
                    if (param.AT == Parameters.AtkType.bow) //bow attacks
                    {
                        if(pState.ReturnGround()) // ground bow attacks
                        {
                            animator.SetBool("IsAttacking", true);
                            animator.SetTrigger(param.BowAttTriggerName[param.wepTypeID]);
                            animator.SetInteger("ComboNum", animator.GetInteger("ComboNum") + 1); //adds 1 to the cur combo count
                        }
                        if(!pState.ReturnGround()) //aerial bow attacks
                        {

                        }
                        
                    }
                    
                }
                break;
        }
        if (!pState.ReturnGround())
        {
            animator.SetBool("OnGround", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("BackToIdle");
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
