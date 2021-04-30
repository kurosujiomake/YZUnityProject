using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpAtk : StateMachineBehaviour
{
    private InputSystemShell pCM;
    private NewSpUltAktParam nSP;
    public float heldTime = 0;
    private float timer = 0;
    public bool isFinisher = false;
    private bool setFinisher = false;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<InputSystemShell>();
        nSP = animator.GetComponent<NewSpUltAktParam>();
        timer = 0;
        setFinisher = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!isFinisher)
        {
            if (pCM.GetButtonHeld("Atk2"))
            {
                timer += Time.deltaTime;
            }
            if(timer >= heldTime)
            {
                animator.SetBool("spAtkHeld", true);
                setFinisher = true;
            }
        }
        
    }
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isFinisher)
        {
            animator.SetBool("spAtkHeld", false);
            animator.ResetTrigger("SpAtk");
            nSP.PutSkillOnCD(0);
            nSP.StartCD(0);
        }
        if(!isFinisher)
        {
            if (!setFinisher)
            {
                animator.SetBool("spAtkHeld", false);
                animator.ResetTrigger("SpAtk");
                nSP.PutSkillOnCD(0);
                nSP.StartCD(0);
            }
                
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
