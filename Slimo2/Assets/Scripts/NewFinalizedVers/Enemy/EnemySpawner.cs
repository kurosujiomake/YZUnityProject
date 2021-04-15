using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyPrefabDatabase eDB;
    public EnemiesBundle[] eBundle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    single,
    continous,
    none
}
