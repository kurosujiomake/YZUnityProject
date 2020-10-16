using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum varType
{
    Bool,
    Float,
    Int,
    Trigger
}
public class SpAtkTriggerSet : StateMachineBehaviour
{
    public VariableBundle[] SpVar;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for(int i = 0; i < SpVar.Length; i++)
        {
            switch(SpVar[i].type)
            {
                case varType.Bool:
                    animator.SetBool(SpVar[i].varName, SpVar[i].boolValue[0]);
                    break;
                case varType.Float:
                    animator.SetFloat(SpVar[i].varName, SpVar[i].floatValue[0]);
                    break;
                case varType.Int:
                    animator.SetInteger(SpVar[i].varName, SpVar[i].intValue[0]);
                    break;
                case varType.Trigger:
                    if(SpVar[i].whenTrigger == 0)
                    animator.SetTrigger(SpVar[i].varName);
                    break;
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < SpVar.Length; i++)
        {
            switch (SpVar[i].type)
            {
                case varType.Bool:
                    animator.SetBool(SpVar[i].varName, SpVar[i].boolValue[1]);
                    break;
                case varType.Float:
                    animator.SetFloat(SpVar[i].varName, SpVar[i].floatValue[1]);
                    break;
                case varType.Int:
                    animator.SetInteger(SpVar[i].varName, SpVar[i].intValue[1]);
                    break;
                case varType.Trigger:
                    if (SpVar[i].whenTrigger == 1)
                        animator.SetTrigger(SpVar[i].varName);
                    break;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < SpVar.Length; i++)
        {
            switch (SpVar[i].type)
            {
                case varType.Bool:
                    animator.SetBool(SpVar[i].varName, SpVar[i].boolValue[2]);
                    break;
                case varType.Float:
                    animator.SetFloat(SpVar[i].varName, SpVar[i].floatValue[2]);
                    break;
                case varType.Int:
                    animator.SetInteger(SpVar[i].varName, SpVar[i].intValue[2]);
                    break;
                case varType.Trigger:
                    if (SpVar[i].whenTrigger == 2)
                        animator.SetTrigger(SpVar[i].varName);
                    break;
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
[System.Serializable]
public class VariableBundle
{
    public string varName;
    public varType type;
    public bool[] boolValue = new bool[3];
    public float[] floatValue = new float[3];
    public int[] intValue = new int[3];
    public int whenTrigger;
}
