using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Area", menuName = "Spawns/Projectiles/Area")]
public class AreaSpawn : BaseProj
{
    public enum AreaType
    {
        circle,
        square,
        other
    }
    public AreaType aType;
    public float Radius;
    public float setupDur;
    public float lifeTime;
    void Awake()
    {
        SpawnType = PType.Area;
    }
}
