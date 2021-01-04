using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTake : MonoBehaviour
{
    public bool isEnemy = false;
    private KnockbackHandler kb;
    public int HitID = -1;
    public DamageNumberMain DnM = null;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Transform>().tag == "Enemy")
        {
            isEnemy = true;
            DnM = GameObject.FindGameObjectWithTag("HitDisplayCanv").GetComponent<DamageNumberMain>();
        }
    }

    void TakeDamage(float dmg, int hitCount)
    {
        //do health removal here
        DnM.DisplayUpdate(dmg, hitCount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isEnemy)
        {
            if(collision.GetComponent<KBInfoPass>() != null) //check if its a valid hitbox
            {
                var h = collision.GetComponent<KBInfoPass>();
                if(h.Hit_ID != HitID)
                {
                    //print("hit with " + h.Damage + " Damage");
                    //print("hit with " + h.HitCount + " Hits");
                    TakeDamage(h.Damage, h.HitCount);
                    HitID = h.Hit_ID;
                }
            }
        }
        
    }
}
