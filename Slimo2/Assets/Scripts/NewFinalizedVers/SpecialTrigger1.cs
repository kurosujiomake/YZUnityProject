using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTrigger1 : MonoBehaviour
{
    [Header("This one turns on/off air dashes and attack")]
    public bool TurnOn = false;
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.GetComponent<Parameters>() != null) //only the player should have this script attached
        {
            var v = c.GetComponent<Parameters>();
            var a = c.GetComponent<Animator>();
            switch(TurnOn)
            {
                case false:
                    v.m_DashMax = 0;
                    a.SetBool("IsGamePlay", false);
                    break;
                case true:
                    v.m_DashMax = 5;
                    a.SetBool("IsGamePlay", true);
                    break;
            }
        }
    }
}
