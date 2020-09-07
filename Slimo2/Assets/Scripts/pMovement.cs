using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pMovement : MonoBehaviour
{
    public enum movementState { player, attacking};
    public movementState mState;
    public enum movetypeState { forced, noMove};
    public movetypeState mTState;
    [SerializeField] private bool IsActive = false;
    //[SerializeField] private bool OnGround = false;
    [SerializeField] private float h = 0;
    [SerializeField] private Rigidbody2D rigid2D;
    private float p_MoveSpeed = 0;
    private float p_JumpPwr = 0;
    //for forced movement only
    private Vector2 newVel2;
    // Start is called before the first frame update
    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(mState)
        {
            case movementState.player:
                h = Input.GetAxis("Horizontal");
                Moving();
                break;
            case movementState.attacking:
                switch(mTState)
                {
                    case movetypeState.forced:

                        break;
                    case movetypeState.noMove:

                        break;
                }
                break;
        }

        
        //print(h);
        switch(IsActive)
        {
            case false:
                ResetVelocity();
                break;
            case true:
                Moving();
                break;
        }
    }
    public void SetValues(float moveSpd, float JumpPwr)
    {
        p_MoveSpeed = moveSpd;
        p_JumpPwr = JumpPwr;
    }
    public void SetValues(bool active)
    {
        IsActive = active;
    }
    public void ResetVelocity()
    {
        rigid2D.velocity = Vector2.zero;
        SetState(1);
    }
    private void Moving()
    {
        Vector2 v = rigid2D.velocity;
        v.x = h * p_MoveSpeed * Time.deltaTime * 100;
        rigid2D.velocity = v;
    }
    public void Jumping()
    {
        Vector2 j = rigid2D.velocity;
        j.y = p_JumpPwr * Time.deltaTime * 100;
        rigid2D.velocity = j;
    }
    public float ReturnH()
    {
        return h;
    }
    //for later moving attack use
    public void SetState(int state)
    {
        if(state == 0)
        {
            mState = movementState.player;
        }
        if(state == 1)
        {
            mState = movementState.attacking;
            mTState = movetypeState.noMove;
        }
        if(state == 2)
        {
            mState = movementState.attacking;
            mTState = movetypeState.forced;
        }
    }
    public void ForcedMove(float m_speed, float m_angle)
    {
        float x = m_angle * Mathf.Deg2Rad;
        newVel2.x = (Mathf.Cos(m_angle) / m_speed) * Time.deltaTime * 100f;
        newVel2.y = (Mathf.Sin(m_angle) / m_speed) * Time.deltaTime * 100f;
        rigid2D.velocity = newVel2;
        
    }

}
