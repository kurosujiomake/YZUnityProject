using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerAttackMove : MonoBehaviour
{
    private PlayerControllerNew pCN = null;
    private GroundChecker g = null;
    public AtkMoveParameters[] atks;
    public string curAtkName;
    private string prevAtkName;
    private Animator pAnimator;
    public float curAnimTime;
    private float a_dir = 0;
    private Rigidbody2D r2D = null;
    public bool TimerStart = false;
    private Animator anim = null;
    private bool paused = false;
    public bool isAttacking = false;
    [Header("Do not input data below, this area is auto populated during gameplay")]
    public AtkMoveParameters curAttack;
    // Start is called before the first frame update
    void Start()
    {
        pAnimator = GetComponent<Animator>();
        r2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pCN = GetComponent<PlayerControllerNew>();
        g = GetComponent<GroundChecker>();
    }
    void FixedUpdate()
    {
        if (TimerStart)
        {
            if(!paused)
            {
                Timer();
            }
            
        }
        if(!TimerStart)
        {
            curAnimTime = 0;
        }
        if (isAttacking)
        {
            if(curAttack != null && curAttack.numOfMovements != 0)
            {
                switch (pCN.facingRight)
                {
                    case true:
                        a_dir = curAttack.Direction[curAttack.curMovement];
                        break;
                    case false:
                        a_dir = 180 - curAttack.Direction[curAttack.curMovement];
                        break;
                }
                if(curAttack.hasPause)
                {
                    if(!g.ReturnGroundCheck() && curAnimTime >= curAttack.whenPause)
                    {
                        anim.enabled = false;
                        paused = true;    
                    }
                    else
                    {
                        anim.enabled = true;
                        paused = false;
                    }
                }
            }
            if(ReturnCanMove())
            {
                Debug.Log("the att should be force moving player");
                ForcedMovement();
                pCN.SetPState(2);
            }
            if(!ReturnCanMove())
            {
                Debug.Log("Cant Move Yet");
                pCN.SetPState(0);
            }
        }
        if(!isAttacking)
        {
            curAtkName = "No Atk";
        }
        if (curAtkName != prevAtkName)
        {
            for (int i = 0; i < atks.Length; i++)
            {
                atks[i].curMovement = 0;
                if (atks[i].atkName == curAtkName)
                {
                    curAttack = atks[i];
                }
            }
            prevAtkName = curAtkName;
        }
    }

    private void ForcedMovement()
    {
        float d = a_dir * Mathf.Deg2Rad;
        float c = curAttack.moveVelocity[curAttack.curMovement];
        Vector2 v = new Vector2(Mathf.Cos(d) * c * Time.deltaTime, Mathf.Sin(d) * c * Time.deltaTime);
        r2D.velocity = v;
    }
    private bool ReturnCanMove()
    {
        bool cm = false;
        if(curAttack.numOfMovements != 0)
        {
            if (curAnimTime > curAttack.startEnd[curAttack.curMovement].x && curAnimTime <= curAttack.startEnd[curAttack.curMovement].y)
            {
                cm = true;
                
            }
            if(curAnimTime > curAttack.startEnd[curAttack.curMovement].y && curAttack.curMovement < curAttack.numOfMovements - 1)
            {
                curAttack.curMovement++;
            }
        }
        return cm;
    }
    private void Timer()
    {
        curAnimTime += Time.deltaTime;
    }
}
[System.Serializable]
public class AtkMoveParameters
{
    public string atkName;
    public int numOfMovements;
    public int curMovement;
    public float[] Direction;
    public float[] moveVelocity;
    public Vector2[] startEnd;
    public bool hasPause;
    public float whenPause;
}
