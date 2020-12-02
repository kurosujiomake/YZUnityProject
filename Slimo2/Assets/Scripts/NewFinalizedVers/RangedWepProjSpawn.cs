using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWepProjSpawn : MonoBehaviour
{
    public bool FireOne = false;
    public bool FireCont = false;
    public bool FireAll = false;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!FireOne)
            hasFiredOne = false;
        if (!FireCont)
            hasFiredCont = false;

        if(FireOne && !hasFiredOne)
        {
            StartCoroutine(FireOnce(ProjID, angle));
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
    IEnumerator FireOnce(int _projID, float _angle)
    {
        float a = 0;
        float spd = sDB.ReturnBSpeed(_projID);
        switch(GetComponentInParent<PlayerControllerNew>().facingRight) //finds the direction player is facing
        {
            case true:
                a = _angle;
                break;
            case false:
                a = 180 - _angle;
                break;
        }
        a *= Mathf.Deg2Rad; //converts degrees to radians
        GameObject clone = Instantiate(sDB.ReturnSpawnObj(_projID), originPoints[OriginPID].position, Quaternion.identity);
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

    void KBEffectAddon()
    {

    }
}

