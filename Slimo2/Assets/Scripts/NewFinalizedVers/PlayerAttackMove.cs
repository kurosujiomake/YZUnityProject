using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerAttackMove : MonoBehaviour
{
    public AtkMoveParameters[] atks;
    public AtkMoveParameters curAttack;
    public string curAtkName;
    private string prevAtkName;
    public Animator pAnimator;
    public float curAnimTime;
    public bool canMove = false; //for inspector testing purposes, do not use this variable
    private bool b = false;
    private float a_dir = 0;
    private Rigidbody2D r2D = null;
    public bool TimerStart = false;
    // Start is called before the first frame update
    void Start()
    {
        pAnimator = GetComponent<Animator>();
        r2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TimerStart)
        {
            Timer();
        }
        if(!TimerStart)
        {
            curAnimTime = 0;
        }

        //DebugBoolChanges();
        if (pAnimator.GetBool("IsAttacking"))
        {
            if(curAttack != null && curAttack.numOfMovements != 0)
            {
                switch (GetComponent<PlayerConsolidatedControl>().ReturnFacingDir())
                {
                    case true:
                        a_dir = curAttack.Direction[curAttack.curMovement];
                        break;
                    case false:
                        a_dir = 180 - curAttack.Direction[curAttack.curMovement];
                        break;
                }
            }
            canMove = ReturnCanMove();
            if(ReturnCanMove())
            {
                ForcedMovement();
            }
            if(!ReturnCanMove())
            {
                r2D.velocity = Vector2.zero;
            }
        }
        if(!pAnimator.GetBool("IsAttacking"))
        {
            curAnimTime = 0;
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
    private void DebugBoolChanges()
    {
        if(canMove && !b)
        {
            Debug.Log("Can Atk Move");
            b = true;
        }
        if(!canMove && b)
        {
            b = false;
        }
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
}
