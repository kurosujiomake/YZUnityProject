using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Spawns/Projectiles/Bullets")]
public class BulletProj : BaseProj
{
    public float speed;
    public float lifespan;
    public float direction;
    void Awake()
    {
        SpawnType = PType.Bullet;
        
    }

    
}
