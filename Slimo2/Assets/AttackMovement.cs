using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMovement : StateMachineBehaviour
{
    public Parameters param;
    [Header("Please have the number of variables the same")]
    public int numberOfMovements = 0;
    [SerializeField]
    private int curMovement = 0;
    public float[] force;
    [Header("Uses degrees")]
    public float[] Direction;
    [Header("Use animation current time")]
    public Vector2[] startEnd;
    public bool facingRight = true;
    private float m_dir = 0;
    private Rigidbody2D r2D = null;
    [SerializeField]
    private float curAnimTime = 0;
    public PlayerAttackMove m_pAM = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        param = animator.GetComponent<Parameters>();
        m_pAM = animator.GetComponent<PlayerAttackMove>();
        curMovement = 0;
        r2D = animator.GetComponent<Rigidbody2D>();

        facingRight = animator.GetComponent<PlayerConsolidatedControl>().ReturnFacingDir();
        switch (facingRight)
        {
            case true:
                m_dir = Direction[curMovement];
                break;
            case false:
                m_dir = 180 - Direction[curMovement];
                break;
        }
        r2D.velocity = Vector2.zero; //freezes any prior player movment at enter
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        curAnimTime = stateInfo.length;
        if(stateInfo.normalizedTime >= startEnd[curMovement].x && stateInfo.normalizedTime <= startEnd[curMovement].y)
        {

            DirectionalMove(curMovement);
            Debug.Log("We are moving!");
        }
        if(stateInfo.normalizedTime > startEnd[curMovement].y && curMovement < numberOfMovements - 1)
        {
            r2D.velocity = Vector2.zero;
            curMovement++;
        }
                
            
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        curMovement = 0;
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

    private void DirectionalMove(int _curMove)
    {
        float dir = m_dir * Mathf.Deg2Rad;
        Vector2 v = new Vector2(Mathf.Cos(dir) * force[_curMove] * Time.deltaTime, Mathf.Sin(dir) * force[_curMove] * Time.deltaTime);
        r2D.velocity = v;
    }
    
}
