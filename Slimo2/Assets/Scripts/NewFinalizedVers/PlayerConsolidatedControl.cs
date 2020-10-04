using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsolidatedControl : MonoBehaviour
{
    private pStates m_pSt;
    private Parameters m_param;
    public bool m_isGrounded = false;
    [SerializeField]
    private bool m_facingRight = true;
    private Rigidbody2D m_rb = null;
    private bool m_jumpPressed = false;
    private Animator anim = null;
    [SerializeField] private bool m_canMove = true;
    private bool m_atkPressed = false;
    private enum m_MoveType {player, none, forced};
    [SerializeField]
    private m_MoveType m_MType;
    [SerializeField]
    private bool m_Dashing = false;
    [SerializeField]
    private bool m_aDashing = false;
    [SerializeField]
    private int m_curADashes = 0;
    [SerializeField]
    private bool m_BTP = false;
    public SpriteAfterImage[] m_dashEffectScripts = new SpriteAfterImage[4];

    //private bool m_canGDash = true;
    void Awake()
    {
        m_pSt = GetComponent<pStates>();
        m_param = GetComponent<Parameters>();
        m_rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SetLocks(0);
    }
    public bool ReturnCanMove()
    {
        return m_canMove;
    }
    void FixedUpdate() //remember to always use fixed update for controls or else stutter happens
    {
        //things that should be running regardless of what state it is currently
        VarResets();
        GroundChecking();
        MoveCheck();
        switch(m_MType)
        {
            case m_MoveType.forced:
                break;
            case m_MoveType.player:
                if(m_isGrounded)
                {
                    Jump();
                    GroundMovement();
                    GroundDashing();
                }
                if(!m_isGrounded)
                {
                    AirMovement();
                    AirDashing();
                }
                break;
            case m_MoveType.none:
                //currently nothing should be happening
                break;
        }
    }
    public bool ReturnFacingDir()
    {
        return m_facingRight;
    }
    private string StickDir() //returns which current direction the stick is pointed at
    {
        string d = "neutral";
        if (Input.GetAxisRaw("Vertical") < m_param.VertBuffer && Input.GetAxisRaw("Vertical") > -m_param.VertBuffer) //ignores some small inputs in vertical stick
        {
            if(Input.GetAxisRaw("Horizontal") >= m_param.HoriBuffer)
            {
                d = "right";
                m_param.HorV = 0;
            }
            if(Input.GetAxisRaw("Horizontal") <= -m_param.HoriBuffer)
            {
                d = "left";
                m_param.HorV = 0;
            }
        }
        if(Input.GetAxisRaw("Horizontal") < m_param.HoriBuffer && Input.GetAxisRaw("Horizontal") > -m_param.HoriBuffer) //ignores small inputs in horizontal stick
        {
            if(Input.GetAxisRaw("Vertical") >= m_param.VertBuffer)
            {
                d = "up";
                m_param.HorV = 1;
            }
            if(Input.GetAxisRaw("Vertical") <= -m_param.VertBuffer)
            {
                d = "down";
                m_param.HorV = 1;
            }
        }
        return d;
    }
    private void FreezeMovement()
    {
        m_rb.velocity = Vector2.zero; // freezes movement
    }
    private void VarResets() //some conditional variable resets
    {
        m_param.facingRight = m_facingRight;
        if(Input.GetAxis("Jump") == 0) //resets jump button input
        {
            m_jumpPressed = false;
        }
        m_param.m_jumpPressed = m_jumpPressed;
        if(m_isGrounded)
        {
            m_curADashes = m_param.m_DashMax;
            m_param.m_canADash = true;
        }
        if(Input.GetAxis("Dash") == 0)
        {
            m_BTP = false;
        }
    }
    private void MoveCheck()
    {
        if(anim.GetBool("IsAttacking")) //no input at all from the player
        {
            m_MType = m_MoveType.none;
        }
        if(!anim.GetBool("IsAttacking") && !m_Dashing && !m_aDashing) //sets the state back to player input control
        {
            m_MType = m_MoveType.player;
        }
        m_param.m_isDashing = m_Dashing;
        m_param.m_isADashing = m_aDashing;
    }
    private void GroundChecking()
    {
        m_isGrounded = m_pSt.ReturnGround();
    }
    private void GroundMovement() //moving on the ground
    {
        float hori = Input.GetAxis("Horizontal"); //get the input so we can just type hori
        Vector2 v = m_rb.velocity;
        Quaternion t = transform.localRotation;
        v.x = hori * m_param.GHorizontalSpeed * Time.deltaTime * 100;
        m_rb.velocity = v;
        if(hori > 0 && !m_facingRight) //flips player sprite
        {
            t.y = 0;
            transform.localRotation = t;
            m_facingRight = !m_facingRight;
        }
        if(hori < 0 && m_facingRight)
        {
            t.y += -180;
            transform.localRotation = t;
            m_facingRight = !m_facingRight;
        }
    }
    private void AirDashing()
    {
        if(StickDir() == "neutral")
        {
            if(Input.GetAxisRaw("Dash") != 0 && m_param.m_canBTP && !m_BTP)
            {
                m_param.m_canBTP = false;
                m_BTP = true;
                m_MType = m_MoveType.forced;
                foreach (SpriteAfterImage sp in m_dashEffectScripts) //stops the dash afterimage effect
                {
                    sp.SpriteRotReset();
                    sp.StopTrail();
                }
                m_param.m_isTPing = true;
                StartCoroutine(BlinkTele());
            }
        }
        else
        {
            if(Input.GetAxisRaw("Dash") != 0 && m_param.m_canADash && !m_param.m_isTPing)
            {
                m_param.m_canADash = false;
                m_aDashing = true;
                
                m_MType = m_MoveType.forced;
                foreach (SpriteAfterImage sp in m_dashEffectScripts) //should start the dash afterimage effect
                {
                    sp.SpriteRotReset();
                    sp.StartTrail();
                }
                StartCoroutine(AerialDash(StickDir(), m_param.m_ADTime));
            }
        }
    }
    private IEnumerator BlinkCD()
    {
        yield return new WaitForSeconds(m_param.m_btCD);
        m_param.m_canBTP = true;
    }
    private void GroundDashing()
    {
        if(Input.GetAxisRaw("Dash") != 0 && m_param.m_canGDash && !m_param.m_isTPing)
        {
            m_param.m_canGDash = false;
            m_Dashing = true;
            m_MType = m_MoveType.forced; 
            foreach(SpriteAfterImage sp in m_dashEffectScripts) //should start the dash afterimage effect
            {
                sp.SpriteRotReset();
                sp.StartTrail();
            }
            if(m_facingRight)
            {
                StartCoroutine(GroundDash("right", m_param.m_GDTime));
                
            }
            if(!m_facingRight)
            {
                StartCoroutine(GroundDash("left", m_param.m_GDTime));
                
            }
        }
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
    private IEnumerator BlinkTele()
    {
        m_param.m_isTPing = true;
        foreach (SpriteAfterImage sp in m_dashEffectScripts) //stops the dash afterimage effect
        {
            sp.StopTrail();
        }
        m_MType = m_MoveType.forced;
        SetLocks(3);
        yield return new WaitForSeconds(m_param.m_btTime);
        GameObject tar = ValidTeleCheck(m_param.m_maxTeleDist, m_param.m_teleTargetTag);
        //adds a bit of randomnless to the offset
        float ranX = Random.Range(-m_param.m_randNoise[0], m_param.m_randNoise[0]); //rands X
        float ranY = Random.Range(-m_param.m_randNoise[1], m_param.m_randNoise[1]); //rands Y
        Vector3 randV = new Vector3(ranX, ranY, 0);
        if(tar.transform.position.x - transform.position.x > 0 && tar != gameObject) //tele target is on the right side of player and is not the player
        {
            transform.position = tar.transform.position + m_param.m_offSet[1] + randV;
        }
        if(tar.transform.position.x - transform.position.x < 0 && tar != gameObject) //tele target is on the left side
        {
            transform.position = tar.transform.position + m_param.m_offSet[0] + randV;
        }
        if(tar == gameObject) //if there is no valid tele target close enough
        {
            transform.position = tar.transform.position; //sets player to target location
        }
        m_param.m_isTPing = false;
        m_MType = m_MoveType.player;
        SetLocks(0);
        StartCoroutine(BlinkCD());
    }
    private IEnumerator GroundDash(string dir, float duration)
    {
        ForceMove(dir, m_param.m_GDSpd);
        //print("Dash start" + m_rb.velocity.x.ToString());
        yield return new WaitForSeconds(duration);
        //print("Dash end");
        foreach(SpriteAfterImage sp in m_dashEffectScripts) //stops the dash afterimage effect
        {
            sp.StopTrail();
        }
        m_Dashing = false;
        SetLocks(0);
        StartCoroutine(GDCD());
    }
    private IEnumerator AerialDash(string dir, float duration)
    {
        m_curADashes--;
        ForceMove(dir, m_param.m_ADSpd);
        yield return new WaitForSeconds(duration);
        foreach (SpriteAfterImage sp in m_dashEffectScripts) //stops the dash afterimage effect
        {
           sp.StopTrail();
        }
        m_aDashing = false;
        SetLocks(0);
        AerialDashCounter();
    }
    private void AerialDashCounter()
    {
        if(m_curADashes > 0)
        {
            m_param.m_canADash = true;
        }
    }
    private IEnumerator GDCD()
    {
        yield return new WaitForSeconds(m_param.m_GDCD);
        m_param.m_canGDash = true;
    }
    private void AirMovement() //same as ground move but slightly slower and no turning
    {
        float hori = Input.GetAxis("Horizontal");
        Vector2 v = m_rb.velocity;
        Vector3 t = transform.localScale;
        v.x = hori * m_param.GHorizontalSpeed * Time.deltaTime * m_param.AHoriSpdMulti;
        m_rb.velocity = v;
    }
    private void Jump() //all jumping consolidated
    {
        float jmp = Input.GetAxis("Jump");
        Vector2 v = m_rb.velocity;
        if(jmp != 0 && !m_jumpPressed) //jump pressed variable is used to turn getaxis into a getaxis down
        {
            m_jumpPressed = true;
            v.y = m_param.VerticalSpeed * Time.deltaTime * 100;
            m_rb.velocity = v;
        }
    }
    private void ForceMove(string dir, float spd)
    {
        Vector2 v = Vector2.zero;
        switch(dir)
        {
            case "right":
                v.x = spd * Time.deltaTime * 100;
                SetLocks(1);
                m_rb.velocity = v;
                break;
            case "left":
                v.x = -spd * Time.deltaTime * 100;
                SetLocks(1);
                m_rb.velocity = v;
                break;
            case "up":
                v.y = spd * Time.deltaTime * 50;
                SetLocks(2);
                m_rb.velocity = v;
                break;
            case "down":
                v.y = -spd * Time.deltaTime * 100;
                SetLocks(2);
                m_rb.velocity = v;
                break;
        }
    }
    void SetLocks(int axis) //constratins certain movement. 0 is reset locks, 1 is lock vertical, 2 is lock horizontal, 3 is lock all 
    {
        if (axis == 1) // freezes y axis
        {
            m_rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            
        }
        if (axis == 2) // freezes x axis
        {
            m_rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            
        }
        if (axis == 3) //for testing purposes and future proofing, this should not be used 
        {
            m_rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
        }
        if (axis == 0)
        {
            m_rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            
        }
    }
}
