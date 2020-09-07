using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public enum e_PlayerStates { Idle, GroundMoving, InAir, Attacking, Flinch, Downed};
    public e_PlayerStates p_State;
    private GroundChecker gCheck;
    private Animator p_anim;
    public pMovement p_move;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private float h = 0;
    [SerializeField] private float playerMoveSpd = 0;
    [SerializeField] private float playerJumpPwr = 0;
    private bool JumpPressed = false;
    [SerializeField] private bool canDoubleJump = false;
    private bool facingRight = true;
    // Start is called before the first frame update
    void Awake()
    {
        p_anim = GetComponent<Animator>();
        p_move = GetComponent<pMovement>();
        gCheck = GetComponent<GroundChecker>();
        p_move.SetValues(playerMoveSpd, playerJumpPwr);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateThings();
        switch (p_State)
        {
            case e_PlayerStates.Idle:
                canDoubleJump = true;
                if (p_anim.GetCurrentAnimatorStateInfo(0).IsTag("Att"))
                { 
                    p_State = e_PlayerStates.Attacking;
                }
                AnimationSetter("State", 0);
                if(!isGrounded)
                {
                    p_State = e_PlayerStates.InAir;
                }
                p_move.SetValues(true);
                if(h != 0)
                {
                    if(isGrounded)
                    {
                        p_State = e_PlayerStates.GroundMoving;
                    }
                    if(!isGrounded)
                    {
                        p_State = e_PlayerStates.InAir;
                    }
                    
                }
                if(Input.GetAxis("Jump") != 0)
                {
                    if(!JumpPressed)
                    {
                        p_move.Jumping();
                        JumpPressed = true;
                    }
                }
                if(Input.GetAxis("Jump") == 0)
                {
                    JumpPressed = false;
                }
                
                break;
            case e_PlayerStates.GroundMoving:
                canDoubleJump = true;
                if (p_anim.GetCurrentAnimatorStateInfo(0).IsTag("Att"))
                {
                    p_State = e_PlayerStates.Attacking;
                }
                p_move.SetValues(true);
                AnimationSetter("State", 1);
                if(h > 0)
                {
                    if(!facingRight)
                    {
                        Flip();
                    }

                }
                if(h < 0)
                {
                    if(facingRight)
                    {
                        Flip();
                    }
                }
                if(!isGrounded)
                {
                    p_State = e_PlayerStates.InAir;
                }
                if(h == 0)
                {
                    if(isGrounded)
                    {
                        p_State = e_PlayerStates.Idle;
                    }
                    if(!isGrounded)
                    {
                        p_State = e_PlayerStates.InAir;
                    }
                }
                if (Input.GetAxis("Jump") != 0)
                {
                    if (!JumpPressed)
                    {
                        p_move.Jumping();
                        JumpPressed = true;
                    }
                }
                if (Input.GetAxis("Jump") == 0)
                {
                    JumpPressed = false;
                }
                break;
            case e_PlayerStates.Attacking:
                p_move.SetValues(false);
                if(!p_anim.GetCurrentAnimatorStateInfo(0).IsTag("Att"))
                {
                    if(isGrounded)
                    {
                        if(h == 0)
                        {
                            p_State = e_PlayerStates.Idle;
                        }
                        if(h != 0)
                        {
                            p_State = e_PlayerStates.GroundMoving;
                        }
                    }
                    if(!isGrounded)
                    {
                        p_State = e_PlayerStates.InAir;
                    }
                    
                }
                break;
            case e_PlayerStates.InAir:
                p_move.SetValues(true);
                AnimationSetter("State", 2);
                if(isGrounded)
                {
                    if(h == 0)
                    {
                        p_State = e_PlayerStates.Idle;
                    }
                    if(h != 0)
                    {
                        p_State = e_PlayerStates.GroundMoving;
                    }

                }
                if (Input.GetAxis("Jump") != 0)
                {
                    if (!JumpPressed && canDoubleJump)
                    {
                        p_move.Jumping();
                        JumpPressed = true;
                        canDoubleJump = false;
                    }
                }
                if (Input.GetAxis("Jump") == 0)
                {
                    JumpPressed = false;
                }
                break;
        }

    }
    public void ResetVel()
    {
        p_move.ResetVelocity();
    }
    public void ForceMove(float c_speed, float c_angle)
    {
        p_move.SetState(2);
        p_move.ForcedMove(c_speed, c_angle);
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tScale = transform.localScale;
        tScale.x *= -1;
        transform.localScale = tScale;
    }
    private void UpdateThings()
    {
        h = p_move.ReturnH();
        isGrounded = gCheck.ReturnGroundCheck();
    }
    public void AnimationSetter(string trigger)
    {
        p_anim.SetTrigger(trigger);
    }
    public void AnimationSetter(string intName, int value)
    {
        p_anim.SetInteger(intName, value);
    }
    public void AnimationSetter(string boolName, bool value)
    {
        p_anim.SetBool(boolName, value);
    }
}
