using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWepProjSpawn : MonoBehaviour
{
    public DmgBloc bloc;
    public bool FireOne = false;
    public bool FireCont = false;
    public bool FireAll = false;
    public bool FireAShower = false;
    public int MaxProj = 0;
    public float projDelay = 0;
    public SpawnDatabase sDB;
    public bool UseSkillID = false;
    public int ProjID = 0;
    public int KBNum = 0;
    public Transform[] originPoints;
    public int OriginPID = 0;
    public int OriginPID2 = 0;
    public bool useMultipleOrigins = false;
    public float angle = 0;
    public float angle2 = 0;
    public bool useMultipleAngles = false;
    public float deviation = 0;
    private int[] HitIDs = new int[2];
    private bool hasFiredOne = false;
    private bool hasFiredCont = false;
    private bool hasFiredShower = false;
    private bool hasFiredAll = false;
    public float arrowShowerRandx;
    public float arrowShowerRandy;
    public float arrowShowerProjSpd;
    public float arrowShowerProjDur;
    public GameObject aShowerSpawner;
    public float[] triAngles;
    // Update is called once per frame
    void Update()
    {
        if (!FireOne)
            hasFiredOne = false;
        if (!FireCont)
            hasFiredCont = false;
        if (!FireAShower)
            hasFiredShower = false;
        if (!FireAll)
            hasFiredAll = false;
        if (FireOne && !hasFiredOne)
        {
            StartCoroutine(FireOnce(ProjID));
            hasFiredOne = true;
        }
        if (FireCont && !hasFiredCont)
        {
            StartCoroutine(FireMultiple());
            hasFiredCont = true;
        }
        if(FireAShower && !hasFiredShower)
        {
            StartCoroutine(ArrowShower());
            hasFiredShower = true;
        }
        if(FireAll && !hasFiredAll)
        {
            StartCoroutine(FireAllAtOnce());
            hasFiredAll = true;
        }
        
    }
    int IDRandomizer()
    {
        int i = 0;
        bool hasSetID = false;
        while (!hasSetID)
        {
            int j = Random.Range(0, 100);
            if (j != HitIDs[0] && j != HitIDs[1])
            {
                HitIDs[0] = HitIDs[1];
                HitIDs[1] = j;
                i = j;
                hasSetID = true;
            }
        }
        return i;
    }
    IEnumerator FireOnce(int _projID)
    {
        float a = 0, ad = 0;
        float spd = sDB.ReturnBSpeed(_projID);
        switch (GetComponentInParent<PlayerControllerNew>().facingRight) //finds the direction player is facing
        {
            case true:
                a = (Random.Range(-deviation, deviation) + angle);
                ad = a * Mathf.Deg2Rad;
                break;
            case false:
                a = (180 - (Random.Range(-deviation, deviation) + angle));
                ad = a * Mathf.Deg2Rad;
                break;
        }
        a *= Mathf.Deg2Rad; //converts degrees to radians
        GameObject clone = Instantiate(sDB.ReturnSpawnObj(_projID), originPoints[OriginPID].position, Quaternion.Euler(0, 0, ad));
        clone.GetComponent<KBInfoPass>().Hit_ID = IDRandomizer();
        clone.GetComponent<KBInfoPass>().curKBNum = KBNum;
        switch (clone.GetComponent<ProjType>().Proj_Type)
        {
            case ProjType.Type.Bullet:
                clone.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(a) * spd, Mathf.Sin(a) * spd);
                clone.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(_projID));
                break;
            case ProjType.Type.Area:

                break;
            case ProjType.Type.Other:

                break;
        }

        yield return new WaitForSeconds(0);
    }
    IEnumerator FireMultiple()
    {
        bool b = true;
        int i = 0;
        int im = MaxProj;
        if (im <= 0) //checks if it is to fire anything at all
            b = false;
        while (b)
        {
            float spd = sDB.ReturnBSpeed(ProjID);
            float d = 0;
            float ed = 0;
            switch (useMultipleAngles)
            {
                case false:
                    switch (GetComponentInParent<PlayerControllerNew>().facingRight) //adjusts angle based on player facing direction and adds random deviation
                    {
                        case true:
                            ed = (Random.Range(-deviation, deviation) + angle);
                            d = ed * Mathf.Deg2Rad;
                            break;
                        case false:
                            ed = (180 - (Random.Range(-deviation, deviation) + angle));
                            d = ed * Mathf.Deg2Rad;
                            break;
                    }
                    GameObject Clone = Instantiate(sDB.ReturnSpawnObj(ProjID), originPoints[OriginPID].position, Quaternion.Euler(0, 0, ed));
                    Clone.GetComponent<KBInfoPass>().Hit_ID = IDRandomizer();
                    Clone.GetComponent<KBInfoPass>().curKBNum = KBNum;
                    Clone.GetComponent<DamageTransfer>().dmgData.SetValues(bloc.dmgToPass, bloc.isCrit, bloc.eleMod, bloc.hitCount);
                    switch (Clone.GetComponent<ProjType>().Proj_Type)
                    {
                        case ProjType.Type.Bullet:
                            Clone.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(d) * spd, Mathf.Sin(d) * spd);
                            Clone.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(ProjID));
                            break;
                        case ProjType.Type.Area:

                            break;
                        case ProjType.Type.Other:

                            break;
                    }
                    break;
                case true:
                    float d2 = 0;
                    switch (GetComponent<PlayerControllerNew>().facingRight) //adjusts angle based on player facing direction and adds random deviation
                    {
                        case true:
                            d = (Random.Range(-deviation, deviation) + angle) * Mathf.Deg2Rad;
                            break;
                        case false:
                            d = (180 - (Random.Range(-deviation, deviation) + angle)) * Mathf.Deg2Rad;
                            break;
                    }
                    switch (GetComponent<PlayerControllerNew>().facingRight) //adjusts angle 2
                    {
                        case true:
                            d2 = (Random.Range(-deviation, deviation) + angle) * Mathf.Deg2Rad;
                            break;
                        case false:
                            d2 = (180 - (Random.Range(-deviation, deviation) + angle)) * Mathf.Deg2Rad;
                            break;
                    }
                    break;
            }
            yield return new WaitForSeconds(projDelay);
            i++;
            if (i >= im)
            {
                b = false;
            }
        }
    }
    IEnumerator ArrowShower()
    {
        print("spawning arrow shower");
        GameObject c = Instantiate(aShowerSpawner, originPoints[OriginPID].position, Quaternion.identity);
        c.GetComponent<ArrowShowerSpawn>().GetParameters(sDB.ReturnSpawnObj(ProjID), KBNum, MaxProj, arrowShowerRandx,
            arrowShowerRandy, projDelay, arrowShowerProjSpd, arrowShowerProjDur, bloc);
        yield return new WaitForSeconds(0);
    }
    IEnumerator FireAllAtOnce()
    {
        float a = 0, pa = 0; //a is angle, pa is projectile angle
        float spd = sDB.ReturnBSpeed(ProjID);
        for (int i = 0; i < MaxProj; i++)
        {
            switch (GetComponentInParent<PlayerControllerNew>().facingRight)
            {
                case true:
                    pa = Random.Range(-deviation, deviation) + triAngles[i];
                    a = pa * Mathf.Deg2Rad;
                    break;
                case false:
                    pa = 180 - (Random.Range(-deviation, deviation) + triAngles[i]);
                    a = pa * Mathf.Deg2Rad;
                    break;
            }
            GameObject c = Instantiate(sDB.ReturnSpawnObj(ProjID), originPoints[OriginPID].position, Quaternion.Euler(0, 0, pa));
            c.GetComponent<KBInfoPass>().Hit_ID = IDRandomizer();
            c.GetComponent<KBInfoPass>().curKBNum = KBNum;
            c.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(ProjID));
            c.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(a) * spd, Mathf.Sin(a) * spd);
            c.GetComponent<DamageTransfer>().dmgData.SetValues(bloc.dmgToPass, bloc.isCrit, bloc.eleMod, bloc.hitCount);
        }
        yield return new WaitForSeconds(0);
    }
    void PassDmgInfo()
    {

    }

}
[System.Serializable]
public class DmgBloc
{
    public float dmgToPass;
    public bool isCrit;
    public int eleMod;
    public int hitCount;

    public void SetValues(float dmg, bool crit, int ele, int count)
    {
        dmgToPass = dmg;
        isCrit = crit;
        eleMod = ele;
        hitCount = count;
    }
    
}
