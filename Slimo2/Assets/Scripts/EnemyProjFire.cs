using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjFire : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Direction in degrees, this can be set via script/directly/animator or by targeting")]
    public float Direction = 0;
    public Transform Target;
    public bool UseTarget = true;
    public float angle = 0;
    private bool above = true;
    [Header("Source ID should be 0 for players, and 1 for enemies")]
    public int SourceID = 1;
    private int[] HitIDs = new int[2];

    [Header("Variables related to projectile")]
    public SpawnDatabase sDB;
    public int projID = 0;
    public int projAmt = 0;
    public float projSpd = 0;
    public float projLifeTime = 0;
    public float inBetweenDelay = 0;
    public Transform projSpawnPoint;
    [Header("KB_ID determines the knockback from database")]
    public int kbID;
    [Header("Use these Bool(s) to fire the proj")]
    public bool Fire = false;
    private bool hasFired = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetConversion();
        FireReset();
        if(Fire && !hasFired)
        {
            StartCoroutine(FireProjCycle());
            hasFired = true;
        }
    }
    public void AddTarget(Transform tar)
    {
        Target = tar;
    }
    void FireReset()
    {
        if(hasFired && !Fire)
        {
            hasFired = false;
        }
    }
    void TargetConversion()
    {
        if(UseTarget)
        {
            Vector2 tarDir = Target.position - transform.position;
            tarDir = tarDir.normalized;
            float dot = Vector2.Dot(tarDir, transform.right);
            if(Target.position.y > transform.position.y)
            {
                above = true;
            }
            if(Target.position.y < transform.position.y)
            {
                above = false;
            }
            switch(above)
            {
                case true:
                    angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
                    break;
                case false:
                    angle = -1 * (Mathf.Acos(dot) * Mathf.Rad2Deg);
                    break;
            }
            Direction = angle;
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
    IEnumerator FireProjCycle()
    {
        bool f = true;
        int a = 0;
        float d = Direction * Mathf.Deg2Rad;
        float spd = projSpd * Time.deltaTime;
        if(projAmt == 0)
        {
            f = false;
        }
        while (f)
        {
            GameObject clone = Instantiate(sDB.ReturnSpawnObj(projID), projSpawnPoint.position, Quaternion.Euler(0,0, Direction));
            
            clone.GetComponent<KBInfoPass>().SourceID = SourceID;
            clone.GetComponent<KBInfoPass>().Hit_ID = IDRandomizer();
            clone.GetComponent<KBInfoPass>().curKBNum = kbID;
            switch (clone.GetComponent<ProjType>().Proj_Type)
            {
                case ProjType.Type.Bullet:
                    clone.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(d) * spd, Mathf.Sin(d) * spd);
                    clone.GetComponent<KBInfoPass>().StartProjTimer(projLifeTime);
                    break;
                case ProjType.Type.Area:

                    break;
                case ProjType.Type.Other:

                    break;
            }

            a++;
            if(a >= projAmt)
            {
                f = false;
            }
            yield return new WaitForSeconds(inBetweenDelay);
        }
        
    }
}
