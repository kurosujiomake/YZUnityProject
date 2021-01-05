using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAmmoSub : StateMachineBehaviour
{
    public int ArrowsUsed = 0; //how many arrows the att uses
    public int bowChargesUsed = 0; //how much bow charge to actually take off
    public bool isBigShot = false; //big shots use 5 charges per shot instead of 1
    public RangedWepProjSpawn rWPS = null;
    public BowLoadCounter bLC;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rWPS = animator.GetComponentInChildren<RangedWepProjSpawn>();
        bLC = animator.GetComponent<BowLoadCounter>();
        if(bLC.ReturnCurCharge() >= bowChargesUsed)
        {
            rWPS.MaxProj = ArrowsUsed; //sets how many arrows to fire
        }
        if(bLC.ReturnCurCharge() < bowChargesUsed)
        {
            switch(isBigShot)
            {
                case true:
                    rWPS.MaxProj = 1; //always defaults to one if charge is not enough and att is firing big shot
                    break;
                case false:
                    rWPS.MaxProj = bLC.ReturnCurCharge(); //uses up the remaining charges
                    break;
            }
        }
        if(bLC.ReturnCurCharge() == 0)
        {
            rWPS.MaxProj = 0; //if the charge is completely empty
        }
        bLC.SubShots(bowChargesUsed); //substracts the correct amount of charges, make sure this goes at the bottom, so it goes last
        Debug.Log(bLC.ReturnCurCharge());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
