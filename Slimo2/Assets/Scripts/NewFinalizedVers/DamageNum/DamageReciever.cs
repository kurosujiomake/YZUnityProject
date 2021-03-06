﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReciever : MonoBehaviour
{
    public DefensiveStats defStats;
    public StatusHolder[] status;
    public DamageNumberMain dNM = null;
    // Start is called before the first frame update
    private Coroutine bleedC, shockC, slowC, freezeC, burnC, poisonC;
    public GameObject[] OnHitEffects;
    public int hitEffectID;
    public float[] slashRotRand = new float[2];
    public BaseEnemyScript baseScript;
    public EnemyHP eHP = null;
    public Transform particleSpawnPoint = null;
    public bool isPlayer = false;
    public PlayerHP pHP = null;
    void Awake()
    {
        if(!isPlayer)
        {
            dNM = GameObject.FindGameObjectWithTag("HitDisplayCanv").GetComponent<DamageNumberMain>();
            baseScript = GetComponent<BaseEnemyScript>();
            eHP = gameObject.GetComponent<EnemyHP>();
        }
        if(isPlayer)
        {
            pHP = GetComponent<PlayerHP>();
        }
    }
    public void TakeDamage(float finalDmg, int element, bool isCrit, int HitCount, HitType _hType) //this actually takes the dmg after calculations
    {
        switch(isPlayer)
        {
            case true:
                for (int i = 0; i < HitCount; i++)
                {
                    pHP.PlayerTakeDamage(CalcDmg(finalDmg, element, isCrit));
                }
                break;
            case false:
                for (int i = 0; i < HitCount; i++)
                {
                    dNM.DisplayUpdate(CalcDmg(finalDmg, element, isCrit), 1);
                    SpawnHitEffect(_hType);
                    baseScript.GotHurt(finalDmg);
                    eHP.TakeDamage(1, CalcDmg(finalDmg, element, isCrit));
                }
                break;
        }
        
        
    }
    void SpawnHitEffect(HitType hType)
    {
        switch(hType)
        {
            case HitType.Generic:
                break;
            case HitType.Slashing:
                Vector3 r = new Vector3(0, 0, Random.Range(slashRotRand[0], slashRotRand[1]));
                Instantiate(OnHitEffects[1], particleSpawnPoint.position, Quaternion.Euler(r));
                break;
            case HitType.Piercing:
                Instantiate(OnHitEffects[2], particleSpawnPoint.position, Quaternion.identity);
                break;
        }
    }
    public float CalcDmg(float _dmgInput, int _eleType, bool isCrit) //for damage calcs that involve ele type
    {
        float output = 0;
        switch(_eleType)
        {
            case 0: //neutral, only gets reduced by total armor
                output = _dmgInput - (defStats.baseDef * (1 + defStats.defMulti));
                //add bleed activation stuff here when added
                break;
            case 1: //wind, gets reduced by both res and armor
                output = (_dmgInput * (1 - defStats.eleRes[5].resAmt)) - (defStats.baseDef * (1 + defStats.defMulti));
                status[1].isActive = true;
                if(shockC != null)
                    StopCoroutine(shockC);
                shockC = StartCoroutine(Timer(1));
                break;
            case 2: //water, the higher armor multi, the more dmg it does
                output = (_dmgInput * (1 - defStats.eleRes[5].resAmt)) * (1 + defStats.defMulti);
                break;
            case 3: //fire, ignores armor
                output = (_dmgInput * (1 - defStats.eleRes[5].resAmt)); 
                break;
            case 4: //earth, only gets additionally reduced by base armor
                output = (_dmgInput * (1 - defStats.eleRes[5].resAmt)) - (defStats.baseDef);
                break;
            case 5: //arcane, the higher base armor, the more dmg it does
                output = (_dmgInput * (1 - defStats.eleRes[5].resAmt)) + defStats.baseDef;
                break;
        }
        if(status[1].isActive) //if this target has shock applied to it
        {
            output *= (1 + status[1].effect.ReturnMagni());
        }
        if(output <= 0)
        {
            output = 1;
        }
        return output;
    }

    //dont use this function until equip system is fully implemented, just adjust stats in the inspector
    public void UpdateDefenses(float _baseDef, float _defMulti, float[] _eleRes) //make sure the caller always puts a 6 unit array in here
    {
        defStats.defMulti = _defMulti;
        defStats.baseDef = _baseDef;
        for (int i = 0; i < defStats.eleRes.Length; i++)
        {
            defStats.eleRes[i].resAmt = _eleRes[i];
        }
    }

    IEnumerator Timer (int slot)
    {
        yield return new WaitForSeconds(status[slot].effect.ReturnDuration());
        status[slot].isActive = false;
    }
}
[System.Serializable]
public class DefensiveStats
{
    public float baseDef, defMulti;
    public EleTypes[] eleRes = new EleTypes[6]; //5 elements and 1 neutral

}
[System.Serializable]
public class EleTypes
{
    public int index;
    public string Name;
    public float resAmt; //% reduction, up to .95 max
}
[System.Serializable]
public class StatusHolder
{
    public StatusBase effect;
    public bool isActive;
    public int slot;
}