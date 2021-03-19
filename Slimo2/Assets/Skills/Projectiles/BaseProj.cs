using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProj : ScriptableObject
{
    public enum PType //add new spawn obj types here
    {
        Bullet,
        Area,
        Other
    }
    public int P_ID;
    public string P_Name;
    [TextArea(15, 20)]
    public string Description;
    public PType SpawnType;
    public GameObject Prefab;
    public int SourceID;

}
