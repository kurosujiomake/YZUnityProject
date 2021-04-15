using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabse", menuName = "Enemies/Database/DB")]
public class EnemyPrefabDatabase : ScriptableObject
{
    public BaseEnemy[] Enemies;
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Enemies.Length; i++)
        {
            if(Enemies[i].e_ID != i)
            {
                Enemies[i].e_ID = i;
            }
        }
    }
    public void OnBeforeSerialize() { }

    public GameObject ReturnPrefab(int ID)
    {
        GameObject g = null;
        for(int i = 0; i <Enemies.Length; i++)
        {
            if(Enemies[i].e_ID == ID)
            {
                g = Enemies[i].prefab;
            }
        }
        return g;
    }
}
