using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyPrefabDatabase eDB;
    public EnemiesBundle eBundle;
    public bool startSpawn = false;
    private bool hasSpawned = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch(eBundle.spType)
        {
            case SpawnType.none: //doesnt spawn anything, a failsafe just in case
                break;
            case SpawnType.single:
                if(startSpawn && !hasSpawned)
                {
                    SpawnSingle();
                }
                break;
            case SpawnType.continous:

                break;
            case SpawnType.restock:

                break;
        }
    }

    void SpawnSingle()
    {
        if(eBundle.SpawnPoints.Length > 1) //this signifies if it spawns multiple at once
        {
            for (int a = 0; a < eBundle.SpawnPoints.Length; a++)
            {
                Instantiate(eDB.ReturnPrefab(eBundle.EnemyIDs[0]), eBundle.SpawnPoints[a].position, Quaternion.identity);
            }
        }
        if(eBundle.SpawnPoints.Length == 1)
        {
            Instantiate(eDB.ReturnPrefab(eBundle.EnemyIDs[0]), eBundle.SpawnPoints[0].position, Quaternion.identity);
        }
        hasSpawned = true;
    }
}
[System.Serializable]
public class EnemiesBundle
{
    public int[] EnemyIDs;
    public int AmtToSpawn;
    public float timeBetweenSpawns;
    public SpawnType spType;
    public Transform[] SpawnPoints;

}

public enum SpawnType
{
    single, //single spawns onces and the deactivates
    continous, //continous spawns after a certain duration until turned off 
    restock, //restock spawns when the last mob(s) it spawned is killed
    none
}
