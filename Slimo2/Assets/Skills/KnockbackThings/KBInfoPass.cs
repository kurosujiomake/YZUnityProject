using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBInfoPass : MonoBehaviour
{
    public int Hit_ID;
    public int SourceID;
    public bool SetHitID;
    public int curKBNum;
    public bool isProj;
    public int HitCount = 1;
    public float Damage = 251;
    public int eleType;
    public HitType hType;

    [Header("This section is for enemy use")]
    public bool isEnemyHitBox = false;
    public GameObject enemySource;
    public void StartProjTimer(float dur)
    {
        StartCoroutine(Timer(dur));
    }

    IEnumerator Timer(float dur)
    {
        yield return new WaitForSeconds(dur);
        if (isProj)
            Destroy(gameObject);
    }
    
}
[System.Serializable]
public enum HitType
{
    Generic,
    Slashing,
    Piercing
}