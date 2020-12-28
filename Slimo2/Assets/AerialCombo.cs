using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialCombo : StateMachineBehaviour
{
    private PlayerControlManager pCM;
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
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
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
                pCN.SetPState(1);
                switch(pCM.GetDirectionL())
                {
                    case "n":
                    case "u":
                    case "l":
                    case "r":
                    case "ul":
                    case "ur":
                        if (pCM.GetButtonDown("Atk1"))
                        {
                            if (animator.GetInteger("AComboNum") <= maxAirCombo)
                                animator.SetTrigger("AAtt_a");
                        }
                        break;
                    case "d":
                    case "dl":
                    case "dr":
                        if(pCM.GetButtonDown("Atk1"))
                        {
                            animator.SetTrigger("DownAtk");
                            animator.ResetTrigger("AAtt_a");
                        }
                        break;
                }
                
                break;
            case false:
                switch (pCM.GetDirectionL())
                {
                    case "n":
                    case "u":
                    case "l":
                    case "r":
                    case "ul":
                    case "ur":
                        if (pCM.GetButtonDown("Atk1"))
                        {
                            if (animator.GetInteger("AComboNum") <= maxAirCombo)
                                animator.SetTrigger("AAtt_a");
                        }
                        break;
                    case "d":
                    case "dl":
                    case "dr":
                        if (pCM.GetButtonDown("Atk1"))
                        {
                            animator.SetTrigger("DownAtk");
                            animator.ResetTrigger("AAtt_a");
                        }
                        break;
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
