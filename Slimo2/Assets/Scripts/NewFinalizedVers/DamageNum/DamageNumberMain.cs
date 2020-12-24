using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void TotalDmgUpdate(float input) //updates the display text for total damage
    {
        DisplayDmgText[1].Activate();
        DisplayDmgText[1].TxtObj.text = Mathf.RoundToInt(input).ToString() + DisplayDmgText[0].DisplayText;
    }
    public void HitDamageUpdate(float dmg) //updates the display text for hit damage
    {
        DisplayDmgText[0].Activate();
        DisplayDmgText[0].TxtObj.text = Mathf.RoundToInt(dmg).ToString() + DisplayDmgText[0].DisplayText;
    }
    public void HitCounterInc(int input)
    {
        DisplayDmgText[2].Activate();
        DisplayDmgText[2].TxtObj.text = input + DisplayDmgText[2].DisplayText;
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
        print("Started fade countdown");
        yield return new WaitForSeconds(Duration);
        print("Fade time reached, turning off all displays");
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
    public Text TxtObj;
    public bool Active;

    public void Activate()
    {
        Active = true;
        TxtObj.enabled = true;
    }
    public void Deactivate()
    {
        Active = false;
        TxtObj.enabled = false;
    }
}
