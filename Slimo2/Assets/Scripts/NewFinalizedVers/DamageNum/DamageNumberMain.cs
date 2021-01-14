using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DamageNumberMain : MonoBehaviour
{
    public float Duration;
    public float dmgTest;
    public DmgText[] DisplayDmgText = new DmgText[4];
    [SerializeField]
    private float totalDmg;
    [SerializeField]
    private int ComboCounter;

    void Start()
    {
        DeactivateAllText();
    }
    
    
    public void DisplayUpdate(float hitDmg, int hitCount) //other scripts should call this
    {
        StopAllCoroutines();
        ComboCounterInc(hitCount);
        AddTotalDmg(hitDmg);
        HitDamageUpdate(hitDmg);
        StartCoroutine(ComboTimer());
    }
    public void DisplayCrit(bool isCrit)
    {
        switch(isCrit)
        {
            case true:
                DisplayDmgText[3].Activate();
                DisplayDmgText[3].TextObj.text = "CRIT!";
                break;
            case false:
                
                break;
        }
    }
    public void TotalDmgUpdate(float input) //updates the display text for total damage
    {
        DisplayDmgText[1].Activate();
        DisplayDmgText[1].TextObj.text = Mathf.RoundToInt(input).ToString() + DisplayDmgText[1].DisplayText;
    }
    public void HitDamageUpdate(float dmg) //updates the display text for hit damage
    {
        DisplayDmgText[0].Activate();
        DisplayDmgText[0].TextObj.text = Mathf.RoundToInt(dmg).ToString() + DisplayDmgText[0].DisplayText;
    }
    public void HitCounterInc(int input)
    {
        DisplayDmgText[2].Activate();
        DisplayDmgText[2].TextObj.text = input + DisplayDmgText[2].DisplayText;
    }
    public void AddTotalDmg(float input)
    {
        totalDmg += input;
        TotalDmgUpdate(totalDmg);
    }
    public void ResetTotalDmg()
    {
        DisplayDmgText[1].Deactivate();
        totalDmg = 0;
    }
    public void ComboCounterInc(int input)
    {
        ComboCounter += input;
        HitCounterInc(ComboCounter);
    }

    IEnumerator ComboTimer()
    {
        yield return new WaitForSeconds(Duration);
        DeactivateAllText();
        ResetTotalDmg();
        ComboCounter = 0;
    }
    
    void DeactivateAllText()
    {
        foreach (DmgText d in DisplayDmgText)
        {
            d.Deactivate();
        }
    }
    void DeactivateSpecificText(int which)
    {
        DisplayDmgText[which].Deactivate();
    }
}

[System.Serializable]
public class DmgText
{
    public string Name;
    public string DisplayText;
    public TextMeshProUGUI TextObj;
    public bool Active;

    public void Activate()
    {
        Active = true;
        TextObj.enabled = true;
    }
    public void Deactivate()
    {
        Active = false;
        TextObj.enabled = false;
    }
    

}
