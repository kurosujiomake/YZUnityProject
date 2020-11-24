using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnDatabase", menuName = "Spawns/Database")]
public class SpawnDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public BaseProj[] Spawns;
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Spawns.Length; i++)
        {
            if (Spawns[i].P_ID != i)
            {
                Spawns[i].P_ID = i;
            }
        }
    }

    public float ReturnBSpeed(int ID)
    {
        float f = 0;
        if (Spawns[ID] is BulletProj)
        {
            BulletProj b = (Spawns[ID] as BulletProj);
            f = b.speed;
        }
        return f;
    }
    public float ReturnBDir(int ID)
    {
        float f = 0;
        if (Spawns[ID] is BulletProj)
        {
            BulletProj b = (Spawns[ID] as BulletProj);
            f = b.direction;
        }
        return f;
    }
    public GameObject ReturnSpawnObj(int ID)
    {
        return Spawns[ID].Prefab;

    }
    public float ReturnBLifetime(int ID)
    {
        float f = 0;
        if (Spawns[ID] is BulletProj)
        {
            BulletProj b = (Spawns[ID] as BulletProj);
            f = b.lifespan;
        }
        return f;
    }
    public void OnBeforeSerialize()
    {
        
    }
}
