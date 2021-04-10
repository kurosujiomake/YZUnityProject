using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image hpBar;
    public float hpMax;
    public float hpCur;
    private float hpPercent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void SetHPMax (float value)
    {
        hpMax = value;
        GetPercent();
    }
    public void SetHP (float MaxValue, float CurValue)
    {
        hpMax = MaxValue;
        hpCur = CurValue;
        GetPercent();
    }
    public void SetHPCur (float value)
    {
        hpCur = value;
        GetPercent();
    }
    private void GetPercent()
    {
        hpPercent = hpCur / hpMax;
        if (hpPercent >= 1) //is max hp
        {
            hpBar.enabled = false;
        }
        if(hpPercent < 1) //has taken damage
        {
            hpBar.enabled = true;
            hpBar.fillAmount = hpPercent;

        }

    }
}
