using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float pHPMax;
    public float pHPCur;
    public PlayerHpDisplay pHPDisplay;
    void Start()
    {
        pHPCur = pHPMax;
        pHPDisplay = GetComponentInChildren<PlayerHpDisplay>();
        pHPDisplay.SetHP(pHPMax, pHPCur);
    }
    public void PlayerTakeDamage(float amt)
    {
        pHPCur -= amt;
        UpdateHPDisplay();
    }

    void UpdateHPDisplay()
    {
        pHPDisplay.SetHPCur(pHPCur);
    }
}
