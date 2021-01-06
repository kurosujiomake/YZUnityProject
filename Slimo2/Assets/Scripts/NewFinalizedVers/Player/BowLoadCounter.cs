using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowLoadCounter : MonoBehaviour
{
    public Animator anim = null;
    public int BowMax = 0;
    private int BowCurr = 0;
    public GameObject displayObj;
    public TextMeshProUGUI text;
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
        if(GetComponent<Parameters>().AT == Parameters.AtkType.bow)
        {
            displayObj.SetActive(true);
            text.SetText("Bow Charge: " + BowCurr.ToString());
        }
        if(GetComponent<Parameters>().AT != Parameters.AtkType.bow)
        {
            displayObj.SetActive(false);
        }
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
