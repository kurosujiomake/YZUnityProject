using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowLoadCounter : MonoBehaviour
{
    public Animator anim = null;
    public int BowMax = 0;
    private int BowCurr = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetBowCount();
    }
    void SetBowCount()
    {
        anim.SetInteger("BowCharge", BowCurr);
    }

    public void AddShots(int _amt)
    {
        if (BowCurr < BowMax)
            BowCurr += _amt;
        if (BowCurr >= BowMax)
            BowCurr = BowMax;
    }
    public void SubShots(int _amt)
    {
        if (BowCurr > 0)
            BowCurr -= _amt;
        if (BowCurr <= 0)
            BowCurr = 0;
    }
    public void FullReload()
    {
        BowCurr = BowMax;
    }
    public int ReturnCurCharge()
    {
        return BowCurr;
    }
}
