using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackHandler : MonoBehaviour
{
    [SerializeField] private float[] lightKnockback = new float[2];
    [SerializeField] private float[] aerialLightKnockback = new float[2];
    [SerializeField] private float[] upwardsKnockback = new float[2];
    private Rigidbody2D rig;
    [SerializeField] private bool isHanging = false;
    [SerializeField] private Transform gotHitBy = null;
    [SerializeField] private bool gotHitLeft = false;
    [SerializeField] private float hangDelayTime = 0;
    [SerializeField] private float[] effectSpawnRotLimit = new float[2];
    public GameObject slashEffect = null;
    [SerializeField] private float effectDuration = 0;
    private float effectOffset = 0;
    [SerializeField] private float[] effectOffsetRange = new float[4];
    
    // Start is called before the first frame update
    void Awake ()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHanging)
        {
            rig.velocity = Vector2.zero;
        }
    }
    public void WhoHitMe(Transform thing)
    {
        gotHitBy = thing;
        if(gotHitBy != null)
        {
            if (gotHitBy.transform.position.x < transform.position.x)
            {
                gotHitLeft = true;
            }
            if (gotHitBy.transform.position.x > transform.position.x)
            {
                gotHitLeft = false;
            }
        }
        

    }
    void ParticleSpawn()
    {
        StartCoroutine(EffectCycle());
    }
    private IEnumerator EffectCycle()
    {
        float partSpawnRot = Random.Range(effectSpawnRotLimit[0], effectSpawnRotLimit[1]);
        effectOffset = Random.Range(effectOffsetRange[0], effectOffsetRange[1]);
        float effectOffsetY = Random.Range(effectOffsetRange[2], effectOffsetRange[3]);
        GameObject clone = null;
        if (!gotHitLeft)
        {
            Vector3 v = new Vector3(transform.position.x + effectOffset, transform.position.y + effectOffsetY, 0);

            clone = Instantiate(slashEffect, v, Quaternion.Euler(0, 0, partSpawnRot));
        }
        else
        {
            Vector3 v = new Vector3(transform.position.x - effectOffset, transform.position.y + effectOffsetY, 0);

            clone = Instantiate(slashEffect, v, Quaternion.Euler(0, 0, partSpawnRot));
        }
        yield return new WaitForSeconds(effectDuration);
        Destroy(clone.gameObject);
    }
    
    public void KnockBacks(string type, bool away)
    {
        Vector2 v = rig.velocity;
        if(away)
        {
            if(gotHitLeft)
            {
                ParticleSpawn();
                if (type == "Light")
                {
                    v.x = lightKnockback[0] * Time.deltaTime * 100;
                    v.y = lightKnockback[1] * Time.deltaTime * 100;
                    
                }
                if (type == "Heavy")
                {
                    //add heavy combo stuff here
                }
                if (type == "AerialLight")
                {
                    v.x = aerialLightKnockback[0] * Time.deltaTime * 100;
                    v.y = aerialLightKnockback[1] * Time.deltaTime * 100;
                }
                if (type == "Upward")
                {
                    v.x = upwardsKnockback[0] * Time.deltaTime * 100;
                    v.y = upwardsKnockback[1] * Time.deltaTime * 100;
                }
            }
            if(!gotHitLeft)
            {
                ParticleSpawn();
                if (type == "Light")
                {
                    v.x = -lightKnockback[0] * Time.deltaTime * 100;
                    v.y = lightKnockback[1] * Time.deltaTime * 100;
                    
                }
                if (type == "Heavy")
                {
                    //add heavy combo stuff here
                }
                if (type == "AerialLight")
                {
                    v.x = -aerialLightKnockback[0] * Time.deltaTime * 100;
                    v.y = aerialLightKnockback[1] * Time.deltaTime * 100;
                }
                if (type == "Upward")
                {
                    v.x = -upwardsKnockback[0] * Time.deltaTime * 100;
                    v.y = upwardsKnockback[1] * Time.deltaTime * 100;
                }
            }
        }
        if(!away)
        {
            if(gotHitLeft)
            {
                if (type == "Light")
                {
                    v.x = -lightKnockback[0] * Time.deltaTime * 100;
                    v.y = lightKnockback[1] * Time.deltaTime * 100;
                }
                if (type == "Heavy")
                {
                    //add heavy combo stuff here
                }
                if (type == "AerialLight")
                {
                    v.x = -aerialLightKnockback[0] * Time.deltaTime * 100;
                    v.y = aerialLightKnockback[1] * Time.deltaTime * 100;
                }
                if (type == "Upward")
                {
                    v.x = -upwardsKnockback[0] * Time.deltaTime * 100;
                    v.y = upwardsKnockback[1] * Time.deltaTime * 100;
                }
            }
            if(!gotHitLeft)
            {
                if (type == "Light")
                {
                    v.x = lightKnockback[0] * Time.deltaTime * 100;
                    v.y = lightKnockback[1] * Time.deltaTime * 100;
                }
                if (type == "Heavy")
                {
                    //add heavy combo stuff here
                }
                if (type == "AerialLight")
                {
                    v.x = aerialLightKnockback[0] * Time.deltaTime * 100;
                    v.y = aerialLightKnockback[1] * Time.deltaTime * 100;
                }
                if (type == "Upward")
                {
                    v.x = upwardsKnockback[0] * Time.deltaTime * 100;
                    v.y = upwardsKnockback[1] * Time.deltaTime * 100;
                }
            }
        }
        
        
        rig.velocity = v;
    }
    public void KnockBacks(float time)
    {
        StopAllCoroutines();
        StartCoroutine(HangCycle(time));
        
    }
    private IEnumerator HangCycle(float time)
    {
        print("starting hangcycle");
        yield return new WaitForSeconds(hangDelayTime);
        isHanging = true;
        print("isHanging Now");
        yield return new WaitForSeconds(time);
        isHanging = false;
        print("hanging finished");
    }
    
}
