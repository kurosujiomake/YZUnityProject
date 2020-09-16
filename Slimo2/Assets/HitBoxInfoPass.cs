using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxInfoPass : StateMachineBehaviour
{
    public Parameters _param = null;
    [SerializeField]
    private int _hitCount = 0;
    [SerializeField]
    private float _delay = 0;
    [SerializeField]
    private float _dir = 0;
    private float _adjustedDir = 0;
    [SerializeField]
    private float _force = 0;
    public GameObject hitBox = null;
    [SerializeField]
    private int _hitBoxID = 0;
    private DamageValues _dv = null;
    [SerializeField]
    private bool _IdRandomized = false;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _IdRandomized = false;
        _param = animator.GetComponent<Parameters>();
        _dv = _param.HitBoxes[0].GetComponent<DamageValues>();
        while(!_IdRandomized)
        {
            _hitBoxID = Random.Range(1, 99);
            if (_hitBoxID != _param.PrevHitBoxID[0] && _hitBoxID != _param.PrevHitBoxID[1])
            {
                _dv.SetRandomHitID(_hitBoxID);
                _param.PrevHitBoxID[0] = _param.PrevHitBoxID[1]; //pushes the hitbox id up
                _param.PrevHitBoxID[1] = _hitBoxID; //pushes new used hitbox id into the storage
                _IdRandomized = true; //stops the loop
            }
            
        }
        switch(_param.facingRight)
        {
            case true:
                _adjustedDir = _dir;
                break;
            case false:
                _adjustedDir = 180 - _dir;
                break;
        }
        _dv.SetValues(_hitCount, _delay, _adjustedDir, _force, _param.AttackDamage);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _IdRandomized = false;
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
