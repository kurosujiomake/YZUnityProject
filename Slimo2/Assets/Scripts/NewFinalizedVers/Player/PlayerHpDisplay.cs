using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpDisplay : MonoBehaviour
{
    public float displayTime;
    public Image HpDisplay;
    public float hpMax;
    public float hpCur;
    private float hpPercent;
    void Start()
    {
        HpDisplay.enabled = false;
    }
    public void SetHPMax(float value)
    {
        hpMax = value;
        GetPercent();
    }
    public void SetHP(float MaxValue, float CurValue)
    {
        hpMax = MaxValue;
        hpCur = CurValue;
        GetPercent();
    }
    public void SetHPCur(float value)
    {
        hpCur = value;
        GetPercent();
    }
    private void GetPercent()
    {
        hpPercent = hpCur / hpMax;
        if (hpPercent >= 1) //is max hp
        {
            HpDisplay.enabled = false;
        }
        if (hpPercent < 1) //has taken damage
        {
            HpDisplay.enabled = true;
            HpDisplay.fillAmount = hpPercent;

        }
        StopAllCoroutines();
        StartCoroutine(DisplayTimer());

    }
    IEnumerator DisplayTimer()
    {
        yield return new WaitForSeconds(displayTime);
        HpDisplay.enabled = false;
    }
}
