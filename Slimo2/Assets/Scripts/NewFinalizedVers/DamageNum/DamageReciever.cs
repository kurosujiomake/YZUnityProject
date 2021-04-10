using System.Collections;
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
    public BaseEnemyScript baseScript;
    public EnemyHP eHP = null;
    void Awake()
    {
        dNM = GameObject.FindGameObjectWithTag("HitDisplayCanv").GetComponent<DamageNumberMain>();
        baseScript = GetComponent<BaseEnemyScript>();
        eHP = gameObject.GetComponent<EnemyHP>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalcDmg(142, 1, false);
        }

    }
    public void TakeDamage(float finalDmg, int element, bool isCrit, int HitCount) //this actually takes the dmg after calculations
    {
        for (int i = 0; i < HitCount; i++)
        {
            dNM.DisplayUpdate(CalcDmg(finalDmg, element, isCrit), 1);
            SpawnHitEffect();
            baseScript.GotHurt(finalDmg);
            eHP.TakeDamage(1, finalDmg);
        }
        
    }
    void SpawnHitEffect()
    {
        Instantiate(OnHitEffects[hitEffectID], transform.position, Quaternion.identity);
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