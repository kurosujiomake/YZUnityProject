using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class PostAttIdle : StateMachineBehaviour
{
    public Parameters param = null; //gets info from parameter script so you dont have to set every single one here
    public float timer = 0;
    private InputSystemShell pCM = null;
    private PlayerControllerNew pCN = null;
    private GroundChecker g = null;
    [SerializeField] private bool isFinalPATT = false;
    [SerializeField] private bool attPressed = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pCN = animator.GetComponent<PlayerControllerNew>();
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<InputSystemShell>();
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
        pCN.SetPState(1); //players have free move during post att idle state
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pCN.SetPState(1); //players should have free movement in this state
        switch (param.AT) //directional influenced triggers
        {
            case Parameters.AtkType.bow: //directional att triggers for bow
                switch (pCM.GetDirectionL())
                {
                    case "d":
                    case "dl":
                    case "dr":
                        //jumpback att goes here
                        break;
                    case "n":
                    case "l":
                    case "r":
                    case "u":
                    case "ul":
                    case "ur":
                        if (pCM.GetButtonDown("Atk1"))
                        {
                            animator.SetTrigger("GAtt_a");
                        }
                        break;
                }
                break;
            case Parameters.AtkType.sword: //directional att triggers for sword
                switch (pCM.GetDirectionL())
                {
                    case "d":
                    case "dl":
                    case "dr":
                    case "n":
                        if (pCM.GetButtonDown("Atk1"))
                        {
                            animator.SetTrigger("GAtt_a");
                        }
                        break;
                    case "l":
                    case "r":
                        if (pCM.GetButtonDown("Atk1"))
                        {
                            animator.SetTrigger("GDashAtk");
                        }
                        break;
                    case "u":
                    case "ul":
                    case "ur":
                        if (pCM.GetButtonDown("Atk1"))
                        {
                            animator.ResetTrigger("GAtt_a");
                            animator.SetTrigger("SwKnockUp");
                        }
                        break;
                }
                break;
            case Parameters.AtkType.axe: //directional att triggers for axe
                switch (pCM.GetDirectionL())
                {
                    case "d":
                    case "dl":
                    case "dr":
                    case "n":
                    case "l":
                    case "r":
                    case "u":
                    case "ul":
                    case "ur":
                        break;
                }
                break;
            case Parameters.AtkType.spear: //directional att triggers for spear
                switch (pCM.GetDirectionL())
                {
                    case "d":
                    case "dl":
                    case "dr":
                    case "n":
                    case "l":
                    case "r":
                    case "u":
                    case "ul":
                    case "ur":
                        break;
                }
                break;
            case Parameters.AtkType.dagger: //directional att triggers for dagger
                switch (pCM.GetDirectionL())
                {
                    case "d":
                    case "dl":
                    case "dr":
                    case "n":
                    case "l":
                    case "r":
                    case "u":
                    case "ul":
                    case "ur":
                        break;
                }
                break;
            case Parameters.AtkType.wand: //directional att triggers for wand
                switch (pCM.GetDirectionL())
                {
                    case "d":
                    case "dl":
                    case "dr":
                    case "n":
                    case "l":
                    case "r":
                    case "u":
                    case "ul":
                    case "ur":
                        break;
                }
                break;
        }
        if (timer > 0)
            timer -= Time.deltaTime;
        
        if(pCM.GetButtonDown("Atk3"))
        {
            animator.SetTrigger("SwordUlt");
            animator.ResetTrigger("GAtt_a");
            animator.ResetTrigger("SwKnockUp");
            animator.ResetTrigger("GDashAtk");
        }
        if(pCM.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
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
        animator.SetBool("IsDashing", param.m_isDashing);
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("BackToIdle");
        animator.ResetTrigger("SpAtk");
        animator.ResetTrigger("GDashAtk");
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
