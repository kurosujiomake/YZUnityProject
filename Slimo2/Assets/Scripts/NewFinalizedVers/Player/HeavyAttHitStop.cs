using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttHitStop : MonoBehaviour
{
    public bool isHeavyAtkOn = false;
    public HitStop hStop = null;
    public float heavyHitStopDur = 0.2f;
    public bool isDetachedFromPlayer = false;
    
    void Awake()
    {
         hStop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HitStop>();
    }
    public void CheckIfHitStop()
    {
        if(isHeavyAtkOn && !isDetachedFromPlayer)
        {
            hStop.UnitSpecificHitStop(this.gameObject, heavyHitStopDur);
        }
    }
    public void HeavyAtkSet(bool heavy)
    {
        isHeavyAtkOn = heavy;
    }

}
