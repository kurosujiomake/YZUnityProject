using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialCombo : StateMachineBehaviour
{
    private InputSystemShell pCM;
    private PlayerControllerNew pCN;
    private Parameters param;
    private NewAtkMove nAM;
    [SerializeField] 
    private bool isNotAtkState = true;
    private bool comNumSet = false;
    public int maxAirCombo = 3;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        comNumSet = false;
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<InputSystemShell>();
        pCN = animator.GetComponent<PlayerControllerNew>();
        param = animator.GetComponent<Parameters>();
        if(nAM == null)
        {
            nAM = animator.GetComponent<NewAtkMove>();
        }
        switch(isNotAtkState)
        {
            case true:
                nAM.DeactivateAtk();
                break;
            case false:
                nAM.ActivateAtk();
                break; 
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch(isNotAtkState) //setting attack triggers for aerial
        {
            case true:
                if(!param.m_isADashing)
                {
                    pCN.SetPState(1);
                }
                
                if (pCM.GetButtonDown("Atk1"))
                {
                    if (animator.GetInteger("AComboNum") <= maxAirCombo && animator.GetInteger("WepType") == 0)
                        animator.SetTrigger("AAtt_a");
                    if(animator.GetInteger("WepType") == 1)
                    {
                        animator.SetTrigger("AAtt_a");
                    }
                }
                if(pCM.GetButtonDown("Atk2_2"))
                {
                    if(animator.GetComponent<NewSpUltAktParam>().ReturnCanUse(1))
                    {
                        animator.SetTrigger("SpAtk2");
                        animator.ResetTrigger("AAtt_a");
                    }
                }
                break;
            case false:
                if (pCM.GetButtonDown("Atk1"))
                {
                    if (animator.GetInteger("AComboNum") <= maxAirCombo && animator.GetInteger("WepType") == 0)
                        animator.SetTrigger("AAtt_a");
                    if (animator.GetInteger("WepType") == 1)
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
        if(!isNotAtkState && animator.GetInteger("WepType") == 0) //combo doesnt increase when using bow
        {
            animator.SetInteger("AComboNum", animator.GetInteger("AComboNum") + 1);
            
        }
        animator.ResetTrigger("DownAtk");
        animator.ResetTrigger("AAtt_a");


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
