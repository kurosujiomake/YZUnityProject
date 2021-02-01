using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePass : StateMachineBehaviour
{
    public float attDmgMulti; //certain attacks have damage multi of their own
    public float dmgInput; //recives base dmg after calculations from the player
    public float dmgToPass; //passes this info to dmg script on the hitbox, which passes it to the target hit
    public int hitCount;
    public DamageGiver dmgGive;
    public DamageTransfer dmgTrans;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dmgTrans = animator.GetComponentInChildren<DamageTransfer>();
        dmgGive = animator.GetComponent<DamageGiver>();
        dmgInput = dmgGive.statBloc.OuputDmg();
        Debug.Log(dmgInput);
        PassInfo();
    }

    void PassInfo()
    {
        dmgToPass = attDmgMulti * dmgInput;
        dmgTrans.dmgData.SetValues(dmgToPass, dmgGive.statBloc.ReturnIsCrit(), dmgGive.statBloc.ReturnEleMod(), hitCount);
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
