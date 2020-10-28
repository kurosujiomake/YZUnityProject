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
    private int curHitID = 0;
    public float particleDuration = 0;
    public KBDatabase kbDatabase = null;
    public int KBID;
    private bool enemyFacingRight = true;

    // Start is called before the first frame update
    void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }
    //hitCount = how many times it hits, hitDelay = delay between hits
    //dir = direction of knockback in degrees
    
    void StartKBCycle()
    {
        if(!kbDatabase.Aerial(KBID)) //the kb is not a knockup float type
        {
            StopAllCoroutines(); //stops previous knockbacks in case player animation cancels
            StartCoroutine(GroundedKB(KBID));
        }
        if(kbDatabase.Aerial(KBID))
        {
            StopAllCoroutines();
            StartCoroutine(AerialKB(KBID));
        }
    }
    private IEnumerator particleSpawn(float _dur)
    {
        GameObject clone = Instantiate(hitParticles, point.position, Quaternion.identity) as GameObject; //spawns hit particle
        yield return new WaitForSeconds(_dur);
        Destroy(clone);
    }
    private IEnumerator GroundedKB(int _KBID)
    {
        //Debug.Log("Knockback is being applied");
        DirectionalKnockBack(kbDatabase.Dir(_KBID), kbDatabase.Vel(_KBID));
        yield return new WaitForSeconds(kbDatabase.KBDur(_KBID));
        StopMovement();
    }
    private IEnumerator AerialKB(int _KBID)
    {
        Debug.Log("AerialKnockback started");
        StopMovement();
        DirectionalKnockBack(kbDatabase.Dir(_KBID), kbDatabase.Vel(_KBID));
        yield return new WaitForSeconds(kbDatabase.KBDur(_KBID));
        StopMovement();
        StartCoroutine(AerialFloat(_KBID));
    }
    private IEnumerator AerialFloat(int _KBID)
    {
        Debug.Log("AerialFloat started");
        rig2D.gravityScale = 0;
        yield return new WaitForSeconds(kbDatabase.FloatDur(_KBID));
        Debug.Log("Aerial Float ended");
        rig2D.gravityScale = 1;
        StopMovement();
    }
    private void DirectionalKnockBack(float _dir, float _force)
    {
        float dir = _dir;
        if(!enemyFacingRight) //if the player is facing left, flip the angles
        {
            dir = 180 - _dir;
            Debug.Log("Flipped KB angle");
        }
        dir *= Mathf.Deg2Rad;
        Vector2 v = new Vector2(Mathf.Cos(dir) * _force, Mathf.Sin(dir) * _force);
        rig2D.velocity = v;
    }
    private void StopMovement()
    {
        rig2D.velocity = Vector2.zero;
    }
    private void DamageCalculation(float _dmg)
    {
        //subtract hp from hp total
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<KBInfoPass>() != null)
        {
            var h = collision.GetComponent<KBInfoPass>();
            if (collision.GetComponentInParent<PlayerControllerNew>() != null)
            {
                var f = collision.GetComponentInParent<PlayerControllerNew>();
                enemyFacingRight = f.facingRight;
            }
            if(h.Hit_ID != curHitID) //this object has not been hit already
            {
                //Debug.Log("DetectedHitbox");
                KBID = h.KB_ID;
                curHitID = h.Hit_ID;
                StartKBCycle();
                Debug.Log("Got hit with kb id of " + KBID);
            }
        }
    }
}
