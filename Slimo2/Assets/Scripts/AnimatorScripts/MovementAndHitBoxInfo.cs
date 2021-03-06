﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndHitBoxInfo: StateMachineBehaviour
{
    public string AttackName = null;
    //public int[] kbIDs;
    public NewAtkMove nAM = null;
    public int[] HitIDs = new int[2];
    public bool hasSetID = false;
    public KBInfoPass kbInfo = null;
    public bool isHeavyAtk = false;
    public HeavyAttHitStop hHS = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        kbInfo = animator.GetComponentInChildren<KBInfoPass>();
        hHS = animator.GetComponent<HeavyAttHitStop>();
        hasSetID = false;
        if(nAM == null)
        {
            nAM = animator.GetComponent<NewAtkMove>();
        }
        nAM.SetAtkName(AttackName);
        
        if(!hasSetID && kbInfo.SetHitID)
        {
            //kbInfo.KB_ID = kbIDs[kbInfo.curKBNum];
            kbInfo.Hit_ID = IDRandomizer();
        }
        hHS.HeavyAtkSet(isHeavyAtk);
    }
    
    int IDRandomizer()
    {
        int i = 0;
        while(!hasSetID)
        {
            int j = Random.Range(0, 100);
            if(j != HitIDs[0] && j != HitIDs[1])
            {
                HitIDs[0] = HitIDs[1];
                HitIDs[1] = j;
                i = j;
                hasSetID = true;
            }
        }
        return i;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(kbInfo.SetHitID && !hasSetID)
        {
            kbInfo.Hit_ID = IDRandomizer();
        }
        if (kbInfo.SetHitID == false)
        {
            hasSetID = false;
        }
        nAM.ActivateAtk();
    }
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nAM.DeactivateAtk();
        hHS.HeavyAtkSet(false);
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
