using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkProjSpawns : MonoBehaviour
{
    public bool spawnProj = false;
    public int skillID;
    public SkillDatabase db;
    public int KBNum;
    private bool projSpawned = false;
    private int[] HitIDs = new int[2];
    public bool isFinisher = false;
    public SpawnDatabase sDB;
    private float dirCorrected;
    public bool isAerial;
    
    void Update()
    {
        if(!projSpawned)
        {
            if(db.returnHasProj(skillID) && spawnProj)
            {
                SpawnProj();
            }
        }
        if(projSpawned && !spawnProj) //resets the proj spawned counter;
        {
            projSpawned = false;
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
    void SpawnProj()
    {
        int projID = db.returnProjID(skillID);
        int projIDFinisher = db.returnProjFinisherID(skillID);
        float d = sDB.ReturnBDir(projID);
        float df = sDB.ReturnBDir(projIDFinisher);
        GameObject clone = null;
        float spd = 0;
        switch(GetComponentInParent<PlayerControllerNew>().facingRight)
        {
            case true:
                switch (isFinisher)
                {
                    case true:
                        dirCorrected = df * Mathf.Deg2Rad;
                        clone = Instantiate(sDB.ReturnSpawnObj(projIDFinisher), transform.position, Quaternion.identity);
                        clone.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(projIDFinisher));
                        spd = sDB.ReturnBSpeed(projIDFinisher);
                        break;
                    case false:
                        dirCorrected = d * Mathf.Deg2Rad;
                        clone = Instantiate(sDB.ReturnSpawnObj(projID), transform.position, Quaternion.identity);
                        clone.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(projID));
                        spd = sDB.ReturnBSpeed(projID);
                        break;
                }
                clone.GetComponent<KBInfoPass>().Hit_ID = IDRandomizer();
                clone.GetComponent<KBInfoPass>().curKBNum = KBNum;
                clone.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(dirCorrected) * spd, Mathf.Sin(dirCorrected) * spd);
                break;
            case false:
                
                switch (isFinisher)
                {
                    case true:
                        dirCorrected = (180 - df) * Mathf.Deg2Rad;
                        clone = Instantiate(sDB.ReturnSpawnObj(projIDFinisher), transform.position, Quaternion.Euler(0,180,0));
                        clone.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(projIDFinisher));
                        spd = sDB.ReturnBSpeed(projIDFinisher);
                        break;
                    case false:
                        dirCorrected = (180 - d) * Mathf.Deg2Rad;
                        clone = Instantiate(sDB.ReturnSpawnObj(projID), transform.position, Quaternion.Euler(0,180,0));
                        clone.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(projID));
                        spd = sDB.ReturnBSpeed(projID);
                        break;
                }
                clone.GetComponent<KBInfoPass>().Hit_ID = IDRandomizer();
                clone.GetComponent<KBInfoPass>().curKBNum = KBNum;
                clone.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(dirCorrected) * spd, Mathf.Sin(dirCorrected) * spd);
                break;
        }
        projSpawned = true;
    }
    void SpawnAirProj()
    {
        float d = sDB.ReturnBDir(db.returnProjID(skillID));
        int aProjIDFinisher = db.returnAerialProjID(skillID, true);
        int aProjID = db.returnAerialProjID(skillID, false);
        GameObject clone = null;
        float spd = 0;
        switch (GetComponentInParent<PlayerControllerNew>().facingRight)
        {
            case true:
                dirCorrected = d * Mathf.Deg2Rad;
                switch (isFinisher)
                {
                    case true:
                        clone = Instantiate(sDB.ReturnSpawnObj(aProjIDFinisher), transform.position, Quaternion.identity);
                        clone.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(aProjIDFinisher));
                        spd = sDB.ReturnBSpeed(aProjIDFinisher);
                        break;
                    case false:
                        clone = Instantiate(sDB.ReturnSpawnObj(aProjID), transform.position, Quaternion.identity);
                        clone.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(aProjID));
                        spd = sDB.ReturnBSpeed(aProjID);
                        break;
                }
                clone.GetComponent<KBInfoPass>().Hit_ID = IDRandomizer();
                clone.GetComponent<KBInfoPass>().curKBNum = KBNum;
                clone.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(dirCorrected) * spd, Mathf.Sin(dirCorrected) * spd);
                break;
            case false:
                dirCorrected = (180 - d) * Mathf.Deg2Rad;
                switch (isFinisher)
                {
                    case true:
                        clone = Instantiate(sDB.ReturnSpawnObj(aProjIDFinisher), transform.position, Quaternion.Euler(0, 180, 0));
                        clone.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(aProjIDFinisher));
                        spd = sDB.ReturnBSpeed(aProjIDFinisher);
                        break;
                    case false:
                        clone = Instantiate(sDB.ReturnSpawnObj(aProjID), transform.position, Quaternion.Euler(0, 180, 0));
                        clone.GetComponent<KBInfoPass>().StartProjTimer(sDB.ReturnBLifetime(aProjID));
                        spd = sDB.ReturnBSpeed(aProjID);
                        break;
                }
                clone.GetComponent<KBInfoPass>().Hit_ID = IDRandomizer();
                clone.GetComponent<KBInfoPass>().curKBNum = KBNum;
                clone.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(dirCorrected) * spd, Mathf.Sin(dirCorrected) * spd);
                break;
        }
        projSpawned = true;
    }
}
