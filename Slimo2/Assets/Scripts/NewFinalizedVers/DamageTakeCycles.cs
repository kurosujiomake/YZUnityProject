using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakeCycles : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rig2D = null;
    public GameObject hitParticles = null;
    public Transform point = null;
    [SerializeField]
    private int dmgID = 0;
    // Start is called before the first frame update
    void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //hitCount = how many times it hits, hitDelay = delay between hits
    //dir = direction of knockback in degrees
    private IEnumerator HitCycles(int _hitCount, float _hitDelay, float _dir, float _force, float _dmg) 
    {
        int count = _hitCount;
        while(count > 0)
        {
            //do the hit stuff
            DamageCalculation(_dmg);
            DirectionalKnockBack(_dir, _force);
            GameObject clone = Instantiate(hitParticles, point.position, Quaternion.identity) as GameObject; //spawns hit particle
            yield return new WaitForSeconds(_hitDelay);
            Destroy(clone);
            count--;
        }

    }

    private void DirectionalKnockBack(float _dir, float _force)
    {
        float dir = _dir * Mathf.Deg2Rad;
        Vector2 v = new Vector2(Mathf.Cos(dir), Mathf.Sin(dir));
        rig2D.AddForce(v * _force, ForceMode2D.Force);
    }

    private void DamageCalculation(float _dmg)
    {
        //subtract hp from hp total
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<DamageValues>() != null)
        {
            Debug.Log("Got Hit");
            DamageValues dv = collision.GetComponent<DamageValues>();
            if(dv.dmgID != dmgID) // makes it so that the object cannot get hit again by the same hitbox
            {
                
                StartCoroutine(HitCycles(dv.hitCount, dv.hitDelay, dv.dir, dv.force, dv.dmg));
                dmgID = dv.dmgID;
            }
            
        }
    }
}
