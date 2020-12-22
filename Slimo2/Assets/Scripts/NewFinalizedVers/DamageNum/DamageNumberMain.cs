using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumberMain : MonoBehaviour
{
    public float Duration;
    public float dmgTest;
    public DmgText[] DisplayDmgText = new DmgText[5];
    

    void Start()
    {
        DeactivateAllText();
    }
    public void HitDamageUpdate(float dmg)
    {
        DisplayDmgText[0].Activate();
        DisplayDmgText[0].TxtObj.text = Mathf.RoundToInt(dmg).ToString() + DisplayDmgText[0].DisplayText;
    }

    void Update()
    {
        HitDamageUpdate(dmgTest);
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
