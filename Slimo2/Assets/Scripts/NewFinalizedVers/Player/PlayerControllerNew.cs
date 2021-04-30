using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerControllerNew : MonoBehaviour
{
    public InputSystemShell pCM;
    private Parameters m_param;
    private Rigidbody2D r2D;
    private GroundChecker g;
    public bool facingRight = true;
    public SpriteRenderer playerSprite;
    public bool isGrounded;
    public Transform hitBoxContainer;
    [Header("Used for camera smoothing when turning")]
    public Transform[] cameraPoints = new Transform[2];
    public Transform cameraFollowPoint;
    private int cADash = 0;
    private SpriteAfterImage sp;
    private Animator anim = null;
    [SerializeField]
    private GameObject c = null;
    private float distx = 100;
    private float disty = 100;
    public GameObject dashEffect= null;
    private int wepSwapID;
    public PlayerDamageTakeCycles pDTC = null;
    public enum controlType
    {
        player,
        noPlayer,
        freeze
    }
    public enum cutsceneC
    {
        player,
        noPlayer
    }
    public controlType pControl;
    public cutsceneC pContC;
    // Start is called before the first frame update
    void Awake()
    {
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<InputSystemShell>();
        pDTC = GetComponent<PlayerDamageTakeCycles>();
        m_param = GetComponent<Parameters>();
        r2D = GetComponent<Rigidbody2D>();
        g = GetComponent<GroundChecker>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        sp = GetComponentInChildren<SpriteAfterImage>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        ResetGroundAtkTriggers();
        switch(pContC)
        {
            case cutsceneC.player:
                if(pCM.GetButtonDown("SwapWep"))
                {
                    SwapWeps();
                    anim.SetInteger("ComboNum", 1);
                    anim.SetInteger("AComboNum", 1);
                }
                break;
            case cutsceneC.noPlayer:

                break;
        }
        IsNearEnemy();
        switch(m_param.AT)
        {
            case Parameters.AtkType.sword:
                anim.SetInteger("WepType", 0);
                wepSwapID = 0;
                break;
            case Parameters.AtkType.bow:
                anim.SetInteger("WepType", 1);
                wepSwapID = 1;
                break;
        }
        isGrounded = g.ReturnGroundCheck();
        GetComponent<Animator>().SetBool("OnGround", isGrounded);
        m_param.facingRight = facingRight;
        if(isGrounded)
        {
            m_param.m_canJump = true;
            cADash = 0;
            m_param.m_canADash = true;
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
            case controlType.noPlayer: //no player is not supposed to run any code from this script
                
                break;
            case controlType.freeze:
                r2D.velocity = Vector2.zero;
                break;
        }
    }
    void SwapWeps()//change this later when equip detection is working
    {
        switch(m_param.AT)
        {
            case Parameters.AtkType.sword:
                m_param.AT = Parameters.AtkType.bow;
                wepSwapID = 1;
                break;
            case Parameters.AtkType.bow:
                m_param.AT = Parameters.AtkType.sword;
                wepSwapID = 0;
                break;
        }
        GetComponentInChildren<WepSwapIndicator>().SwapWeps(wepSwapID);
    }
    void IsNearEnemy()
    {
        if(c == null)
        {
            c = FindClosest("Enemy");
        }
        if(c != null)
        {
            distx = Mathf.Abs(c.transform.position.x - transform.position.x);
            disty = Mathf.Abs(c.transform.position.y - transform.position.y);
        }
        if(distx < m_param.enemyProx && disty < m_param.enemyProx)
        {
            anim.SetBool("IsNearEnemy", true);
        }
        if(distx > m_param.enemyProx || disty > m_param.enemyProx)
        {
            anim.SetBool("IsNearEnemy", false);
            c = null;
        }
    }
    void GroundMovement()
    {
        Vector2 v = r2D.velocity;
        v.x = pCM.ReturnAxis("left", "hori") * m_param.GHorizontalSpeed;
        if(!m_param.m_isDashing)
            r2D.velocity = v;
        FlipSprite();
        if(pCM.GetButtonDown("Dash"))
        {
            GroundDash();
        }
    }
    void AirMovement()
    {
        Vector2 a = r2D.velocity;
        a.x = pCM.ReturnAxis("left", "hori") * m_param.AHoriSpdMulti;
        if(!m_param.m_isADashing)
            r2D.velocity = a;
        if (pCM.GetButtonDown("Dash"))
        {
            //print("dash pressed");
            if (pCM.GetDirectionL() == "n")
                BlinkTeleport();
            else
            {
                cADash++;
                //print(cADash);
                if(cADash <= m_param.m_DashMax)
                {
                    m_param.m_canADash = true;
                    AirDash();
                }
                if(cADash > m_param.m_DashMax)
                {
                    m_param.m_canADash = false;
                }
                
            }
                
        }
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
            hitBoxContainer.localScale = new Vector3(1, 1, 1);
        }
        if(pCM.GetDirectionL() == "l" && facingRight) //player faces left
        {
            playerSprite.flipX = true;
            facingRight = false;
            cameraFollowPoint.position = cameraPoints[0].position;
            hitBoxContainer.localScale = new Vector3(-1, 1, 1);
        }
    }
    void GroundDash()
    {
        if(m_param.m_canGDash)
        {
            m_param.m_canGDash = false;
            m_param.m_isDashing = true;
            sp.StartTrail(facingRight);
            pControl = controlType.noPlayer;
            pDTC.InvincibilityTimer(m_param.m_DashInvinTime);
            switch (facingRight)
            {
                case true:
                    StartCoroutine(DashCycle("right", m_param.m_GDSpd, m_param.m_GDTime, 0));
                    break;
                case false:
                    StartCoroutine(DashCycle("left", m_param.m_GDSpd, m_param.m_GDTime, 0));
                    break;
            }
        }
    }
    void AirDash()
    {
        m_param.m_isADashing = true;
        pControl = controlType.noPlayer;
        pDTC.InvincibilityTimer(m_param.m_ADashInvinTime);
        sp.StartTrail(facingRight);
        StartCoroutine(DashCycle(pCM.GetDirectionL(), m_param.m_ADSpd, m_param.m_ADTime, 1));
        r2D.gravityScale = 0;
    }
    void BlinkTeleport()
    {
        if(m_param.m_canBTP)
        {
            StartCoroutine(BlinkTele());
            pControl = controlType.noPlayer;
        }
    }
    IEnumerator BlinkTele()
    {
        SetLocks(3);
        m_param.m_canBTP = false;
        m_param.m_isTPing = true;
        yield return new WaitForSeconds(m_param.m_btTime);
        if(transform.position.x < ValidTeleCheck(m_param.m_maxTeleDist, m_param.m_teleTargetTag).transform.position.x)
        {
            transform.position = ValidTeleCheck(m_param.m_maxTeleDist, m_param.m_teleTargetTag).transform.position + m_param.m_offSet[0];
        }
        if(transform.position.x >= ValidTeleCheck(m_param.m_maxTeleDist, m_param.m_teleTargetTag).transform.position.x)
        {
            transform.position = ValidTeleCheck(m_param.m_maxTeleDist, m_param.m_teleTargetTag).transform.position + m_param.m_offSet[1];
        }
        SetLocks(0);
        m_param.m_isTPing = false;
        StartCoroutine(BlinkCD());
        pControl = controlType.player;
    }
    IEnumerator BlinkCD()
    {
        yield return new WaitForSeconds(m_param.m_btCD);
        m_param.m_canBTP = true;
    }
    private GameObject FindClosest(string tag) //used to find closest gameobject based on tag, completely self contained and only need to be called once to use
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float dis = Mathf.Infinity;
        Vector3 pos = transform.position;
        foreach (GameObject go in gos)
        {
            Vector2 diff = go.transform.position - pos;
            float curDis = diff.sqrMagnitude;
            if (curDis < dis)
            {
                closest = go;
                dis = curDis;
            }
        }
        return closest;
    }
    GameObject ValidTeleCheck(float maxDist, string tarTag) //checks for valid tele target
    {
        GameObject tar = FindClosest(tarTag);
        if (tar != null) //if there is a target availble in scene
        {
            Vector2 diff = transform.position - tar.transform.position;
            float normDist = diff.sqrMagnitude;
            if (normDist < maxDist) //if target is within max range it will set it as a valid blink target
            {
                return tar;
            }
            else //if outside max tele range it just blinks on itself
            {
                return gameObject;
            }
        }
        else // if no targets are available, just tele in place
        {
            return gameObject;
        }
    }
    void DashEffect(string dir)
    {
        switch(dir)
        {
            case "u":
                Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, 0));
                break;
            case "d":
                Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, 180));
                break;
            case "l":
                Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, 90));
                break;
            case "r":
                Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, -90));
                break;
            case "ur":
                Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, -45));
                break;
            case "ul":
                Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, 45));
                break;
            case "dr":
                Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, -135));
                break;
            case "dl":
                Instantiate(dashEffect, transform.position, Quaternion.Euler(0, 0, 135));
                break;
        }
    }
    IEnumerator DashCycle(string dir, float force, float dur, int type)//handles all ground and air dash needs
    {
        r2D.velocity = Vector2.zero;
        ForceMove(dir, force);
        DashEffect(dir);
        yield return new WaitForSeconds(dur);
        SetLocks(0);
        m_param.m_isDashing = false;
        m_param.m_isADashing = false;
        sp.StopTrail();
        r2D.gravityScale = 5;
        pControl = controlType.player;
        switch(type)
        {
            case 0:
                StartCoroutine(GDCD());
                break;
            case 1:
                //print("used 1 airdash");
                break;
        }
    }
    IEnumerator GDCD() // grounded dash cooldown
    {
        yield return new WaitForSeconds(m_param.m_GDCD);
        m_param.m_canGDash = true;
    }
    void ForceMove (string dir, float spd)//the part that actually force moves the player for the dashes
    {
        Vector2 v = Vector2.zero;
        switch (dir)
        {
            case "r":
            case "right":
                v.x = spd;
                SetLocks(1);
                r2D.velocity = v;
                break;
            case "l":
            case "left":
                v.x = -spd;
                SetLocks(1);
                r2D.velocity = v;
                break;
            case "u":
            case "up":
                v.y = spd *0.4f;
                SetLocks(2);
                r2D.velocity = v;
                break;
            case "d":
            case "down":
                v.y = -spd;
                SetLocks(2);
                r2D.velocity = v;
                break;
            case "ul":
            case "upleft":
                v.x = -spd * 0.45f;
                v.y = spd * 0.45f;
                r2D.velocity = v;
                break;
            case "ur":
            case "upright":
                v.x = spd * 0.45f;
                v.y = spd * 0.45f;
                r2D.velocity = v;
                break;
            case "dl":
            case "downleft":
                v.x = -spd;
                v.y = -spd;
                r2D.velocity = v;
                break;
            case "dr":
            case "downright":
                v.x = spd;
                v.y = -spd;
                r2D.velocity = v;
                break;
        }
    }
    void SetLocks(int axis)
    {
        switch(axis)
        {
            case 0: // no constraints
                r2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation; 
                break;
            case 1: // freezes y axis
                r2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                break;
            case 2: // freezes x axis
                r2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                break;
            case 3: // freeze everything
                r2D.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
        }
    }
    public void SetPState(int state) //0 = freeze, 1 = player, 2 = no player
    {
        switch(state)
        {
            case 0:
                pControl = controlType.freeze;
                break;
            case 1:
                pControl = controlType.player;
                break;
            case 2:
                pControl = controlType.noPlayer;
                break;
        }
    }
    public void InterruptDash()
    {
        StopAllCoroutines();
    }
    void ResetGroundAtkTriggers()
    {
        if(!g.ReturnGroundCheck())
        {
            anim.ResetTrigger("GDashAtk");
            anim.ResetTrigger("SwKnockUp");
            anim.SetBool("GDashAtkF", false);
        }
    }
}
