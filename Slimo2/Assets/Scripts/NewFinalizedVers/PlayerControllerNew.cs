using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerNew : MonoBehaviour
{
    public PlayerControlManager pCM;
    private Parameters m_param;
    private Rigidbody2D r2D;
    private GroundChecker g;
    public bool facingRight = true;
    public SpriteRenderer playerSprite;
    public bool isGrounded;
    [Header("Used for camera smoothing when turning")]
    public Transform[] cameraPoints = new Transform[2];
    public Transform cameraFollowPoint;
    public enum controlType
    {
        player,
        noPlayer,
        freeze
    }
    public controlType pControl;
    // Start is called before the first frame update
    void Awake()
    {
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
        m_param = GetComponent<Parameters>();
        r2D = GetComponent<Rigidbody2D>();
        g = GetComponent<GroundChecker>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    void FixedUpdate()
    {
        isGrounded = g.ReturnGroundCheck();
        if(isGrounded)
        {
            m_param.m_canJump = true;
            
        }
        if(pCM.GetButtonUp("Jump"))
        {
            m_param.m_jumpPressed = false;
        }
        switch(pControl)
        {
            case controlType.player:
                switch(isGrounded)
                {
                    case true:
                        GroundMovement();
                        Jump();
                        break;
                    case false:
                        AirMovement();
                        break;
                }
                break;
            case controlType.noPlayer:

                break;
            case controlType.freeze:

                break;
        }
    }

    void GroundMovement()
    {
        Vector2 v = r2D.velocity;
        v.x = pCM.ReturnAxis("left", "hori") * m_param.GHorizontalSpeed;
        r2D.velocity = v;
        FlipSprite();
    }
    void AirMovement()
    {
        Vector2 v = r2D.velocity;
        v.x = pCM.ReturnAxis("left", "hori") * m_param.AHoriSpdMulti;
        r2D.velocity = v;
    }
    void Jump()
    {
        if(pCM.GetButtonDown("Jump") && m_param.m_canJump)
        {
            Vector2 v = r2D.velocity;
            v.y = m_param.VerticalSpeed;
            r2D.velocity = v;
            m_param.m_jumpPressed = true;
            m_param.m_canJump = false;
        }
    }
    void FlipSprite()
    {
        if(pCM.GetDirectionL() == "r" && !facingRight) //player faces right
        {
            playerSprite.flipX = false;
            facingRight = true;
            cameraFollowPoint.position = cameraPoints[1].position;
        }
        if(pCM.GetDirectionL() == "l" && facingRight) //player faces left
        {
            playerSprite.flipX = true;
            facingRight = false;
            cameraFollowPoint.position = cameraPoints[0].position;
        }
    }
}
