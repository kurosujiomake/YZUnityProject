using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowProjectile : StateMachineBehaviour
{
    [SerializeField]
    private Parameters m_param = null;
    [SerializeField]
    private PlayerConsolidatedControl m_pCon = null;
    public float m_projDelayTime = 0;
    [SerializeField]
    private float m_timer = 0;
    [SerializeField]
    private int m_bowComboNum = 0;
    private int m_count = 0;
    [SerializeField]
    private float m_delayBetweenShots = 0;
    private bool hasFired = false;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_param = animator.GetComponent<Parameters>();
        m_pCon = animator.GetComponent<PlayerConsolidatedControl>();
        m_timer = m_projDelayTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
