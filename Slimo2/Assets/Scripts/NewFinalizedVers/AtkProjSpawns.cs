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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        GameObject clone = null;
        switch(GetComponentInParent<PlayerControllerNew>().facingRight)
        {
            case true:
                clone = Instantiate(db.returnSpawnObj(skillID), transform.position, Quaternion.identity);
                clone.GetComponent<KBInfoPass>().Hit_ID = IDRandomizer();
                clone.GetComponent<KBInfoPass>().curKBNum = KBNum;
                clone.GetComponent<Rigidbody2D>().velocity = new Vector2(db.returnProjSpd(skillID), 0);
                clone.GetComponent<KBInfoPass>().StartProjTimer(db.returnProjDur(skillID));
                if(isFinisher)
                {
                    clone.transform.localScale = new Vector3(1.7f, 2.8f, 1);
                }
                break;
            case false:
                clone = Instantiate(db.returnSpawnObj(skillID), transform.position, Quaternion.Euler(0, 180, 0));
                clone.GetComponent<KBInfoPass>().Hit_ID = IDRandomizer();
                clone.GetComponent<KBInfoPass>().curKBNum = KBNum;
                clone.GetComponent<Rigidbody2D>().velocity = new Vector2(-db.returnProjSpd(skillID), 0);
                clone.GetComponent<KBInfoPass>().StartProjTimer(db.returnProjDur(skillID));
                if (isFinisher)
                {
                    clone.transform.localScale = new Vector3(1.7f, 2.8f, 1);
                }
                break;
        }
        projSpawned = true;
    }
}
