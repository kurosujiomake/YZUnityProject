using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxInfoPass : StateMachineBehaviour
{
    public int _whichHitBox = 0;
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
    [SerializeField]
    private bool _passedInfoToActiveHitbox = false;
    [Header("Manually input animation name here")]
    [SerializeField]
    private string AnimationName = null;
    [SerializeField]
    private bool isWindup = false;
    [SerializeField]
    private bool timerStarted = false;
    private PlayerAttackMove pAtkMv = null;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _passedInfoToActiveHitbox = false;
        _param = animator.GetComponent<Parameters>();
        _dv = _param.HitBoxes[_whichHitBox].GetComponent<DamageValues>();
        DamagePassToHitbox();
        pAtkMv = animator.GetComponent<PlayerAttackMove>();
        timerStarted = false;
    }
    public void DamagePassToHitbox() //puts it into a function so it can be easily called
    {
        HitBoxReset();
        _IdRandomized = false;
        while (!_IdRandomized)
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
        switch (_param.facingRight) //reverses knockback direction based on y axis
        {
            case true:
                _adjustedDir = _dir;
                break;
            case false:
                _adjustedDir = 180 - _dir;
                break;
        }
        _dv.SetValues(_hitCount, _delay, _adjustedDir, _force, _param.AttackDamage);
        _passedInfoToActiveHitbox = true;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_param.HitBoxes[_whichHitBox].GetComponent<CapsuleCollider2D>().enabled)
        {
            _passedInfoToActiveHitbox = false;
        }
        if (_param.HitBoxes[_whichHitBox].GetComponent<CapsuleCollider2D>().enabled)
        {
            if(!_passedInfoToActiveHitbox)
            {
                DamagePassToHitbox();
            }
        }
        if(isWindup)
        {
            animator.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if (AnimationName != null)
        {
            pAtkMv.curAtkName = AnimationName;
            if (!timerStarted)
            {
                pAtkMv.TimerStart = true;
                pAtkMv.isAttacking = true;
                timerStarted = false;
            }
        }
    }
    private void HitBoxReset()
    {
        _dv.SetValues(0, 0, 0, 0, 0);
        _dv.SetRandomHitID(0);
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _IdRandomized = false;
        pAtkMv.TimerStart = false;
        pAtkMv.isAttacking = false;
        Debug.Log("Timer is stopped");
        HitBoxReset();
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
