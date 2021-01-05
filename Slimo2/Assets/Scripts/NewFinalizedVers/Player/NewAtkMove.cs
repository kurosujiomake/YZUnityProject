using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewAtkMove : MonoBehaviour
{   
    private string AtkName;
    public bool isAttacking = false;
    public AtkMoveParam[] atkMoves;
    public AtkMoveParam curAtk;
    private Rigidbody2D r2D = null;
    private AtkMoveTransferScript atkTransfer = null;
    private PlayerControllerNew pCN = null;
    private GroundChecker g = null;
    private Animator anim = null;
    private float normGravScale;
    public float playerFloatDur = 1;
    public float floatGravScale;
    [SerializeField]
    private float diminishedAirBoost = 0;
    public float boostDiminishAmt = 0;
    [SerializeField]
    private bool startDiminishing = false;
    private bool hasDiminished = false;
    private bool hasResetVel = false;
    public Text DebuggingText;
    void Awake()
    {
        r2D = GetComponent<Rigidbody2D>();
        atkTransfer = GetComponentInChildren<AtkMoveTransferScript>();
        pCN = GetComponent<PlayerControllerNew>();
        g = GetComponent<GroundChecker>();
        anim = GetComponent<Animator>();
        normGravScale = r2D.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (AtkName != curAtk.Name)
        {
            SetNewCurAtk(AtkName);
        }
        switch(isAttacking)
        {
            case true:
                hasResetVel = false; //resets this bool so it can reset the player vel later
                if(curAtk.moveParam[atkTransfer.moveNum].hasPause) //check to see if the attack has a must hit ground to finish condition
                {
                    if(atkTransfer.canPause)
                    {
                        if(!g.ReturnGroundCheck())
                        {
                            anim.enabled = false;
                        }
                        if(g.ReturnGroundCheck())
                        {
                            anim.enabled = true;
                        }
                    }
                }
                if (atkTransfer.moveActive) //calls the function to force move player based on which attack
                {
                    
                    if (atkTransfer.canfollowUpMove && curAtk.moveParam[atkTransfer.moveNum].canFollow) //checks to see if this atk has a conditional followup
                    {
                        if(atkTransfer.followUpMoveStartEnd)
                        {
                            ForcedMovement(curAtk.moveParam[curAtk.moveParam[atkTransfer.moveNum].followMoveNum].direction, curAtk.moveParam[curAtk.moveParam[atkTransfer.moveNum].followMoveNum].velocity);
                        }
                    }
                    if(!atkTransfer.canfollowUpMove || !curAtk.moveParam[atkTransfer.moveNum].canFollow) //force moves the player normally
                    {
                        if(curAtk.moveParam[atkTransfer.moveNum].decreaseAirBoost) //if the current attack has diminishing air boost
                        {
                            DiminishingReturnsAirBoost(curAtk.moveParam[atkTransfer.moveNum].velocity); //gets the new velocity
                            ForcedMovement(curAtk.moveParam[atkTransfer.moveNum].direction, diminishedAirBoost); //uses new velocity instead
                        }
                        if (!curAtk.moveParam[atkTransfer.moveNum].decreaseAirBoost) //if the current attack does not have diminishing air boost
                            ForcedMovement(curAtk.moveParam[atkTransfer.moveNum].direction, curAtk.moveParam[atkTransfer.moveNum].velocity);
                    }
                    
                }
                if (!atkTransfer.moveActive) //freezes the player when attacking
                {
                    pCN.SetPState(0);
                }
                if(curAtk.moveParam[atkTransfer.moveNum].floats) //check to see if this attack makes the player float in the air for a bit
                {
                    StopAllCoroutines();
                    r2D.gravityScale = floatGravScale;
                    StartCoroutine(PlayerFloat());
                }


                break;
            case false:
                if(!hasResetVel)
                {
                    pCN.SetPState(0);
                    hasResetVel = true;
                }
                hasDiminished = false; //resets decrement
                if(g.ReturnGroundCheck()) //resets gravity from float in case the player touches ground before the timer runs out
                {
                    StopAllCoroutines();
                    r2D.gravityScale = normGravScale;
                    startDiminishing = false; //turns off and resets diminishing air boost
                }
                AtkName = "Not Attacking"; //not really used
                break;
        }
        
    }
    void DiminishingReturnsAirBoost(float _baseAirBoost) //for spammable aerial attacks that have diminishing returns the longer you spam them
    {
        if (startDiminishing && !hasDiminished) //if this is turned on start lowering the air boost
        {
            hasDiminished = true;
            if (diminishedAirBoost > 0) //doesnt go below 0
                diminishedAirBoost -= boostDiminishAmt;
            if (diminishedAirBoost <= 0)
                diminishedAirBoost = 0; //makes sure it cant go negative
        }
        if (!startDiminishing) //checks to see if the player has spammed infinite air moves yet
        {
            diminishedAirBoost = _baseAirBoost; //sets o
            startDiminishing = true;
        }
        
        
    }

    IEnumerator PlayerFloat() //puts a timer loop for floating after an air attack
    {
        yield return new WaitForSeconds(playerFloatDur);
        r2D.gravityScale = normGravScale;
    }
    void SetNewCurAtk(string _name) //checks if the players have switched to a new attack, and switches parameters accordingly
    {
        for(int i = 0; i < atkMoves.Length; i++)
        {
            if(atkMoves[i].Name == _name)
            {
                curAtk = atkMoves[i];
            }
        }
    }
    public void SetAtkName(string _name)
    {
        AtkName = _name;
    }
    private void ForcedMovement(float _dir, float _vel) //forced movement for the player
    {
        float d = 0;
        switch(pCN.facingRight) //checks current player facing direction so the force move is based on local direction instead of global
        {
            case true:
                d = _dir * Mathf.Deg2Rad;
                break;
            case false:
                d = (180 - _dir) * Mathf.Deg2Rad;
                break;
        }
        Vector2 v = new Vector2(Mathf.Cos(d) * _vel, Mathf.Sin(d) * _vel);
        r2D.velocity = v;
    }
    public void ActivateAtk()
    {
        isAttacking = true;
        pCN.SetPState(2);
    }
    public void DeactivateAtk()
    {
        isAttacking = false;
        pCN.SetPState(1);
    }
}
[System.Serializable]
public class AtkMoveParam
{
    public string Name;
    public MoveGroup[] moveParam;
}
[System.Serializable]
public class MoveGroup
{
    public int moveNum;
    public float direction;
    public float velocity;
    public bool hasPause;
    public bool canFollow;
    public int followMoveNum;
    public bool floats;
    public bool decreaseAirBoost;
}
