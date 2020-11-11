using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Awake()
    {
        r2D = GetComponent<Rigidbody2D>();
        atkTransfer = GetComponentInChildren<AtkMoveTransferScript>();
        pCN = GetComponent<PlayerControllerNew>();
        g = GetComponent<GroundChecker>();
        anim = GetComponent<Animator>();
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
                if(curAtk.moveParam[atkTransfer.moveNum].hasPause)
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
                if (atkTransfer.moveActive)
                {
                    pCN.SetPState(2);
                    ForcedMovement(curAtk.moveParam[atkTransfer.moveNum].direction, curAtk.moveParam[atkTransfer.moveNum].velocity);
                }
                if (!atkTransfer.moveActive)
                {
                    pCN.SetPState(0);
                }
                break;
            case false:
                pCN.SetPState(1);
                AtkName = "Not Attacking";
                break;
        }
        
    }
    void SetNewCurAtk(string _name)
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
    private void ForcedMovement(float _dir, float _vel)
    {
        float d = 0;
        switch(pCN.facingRight)
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
    }
    public void DeactivateAtk()
    {
        isAttacking = false;
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
}
