using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/BasicEnemy")]
public class BaseEnemy : ScriptableObject
{
    public string name;
    public int e_ID;
    public GameObject prefab;
}
