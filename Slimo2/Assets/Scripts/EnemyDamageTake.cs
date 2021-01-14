using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTake : MonoBehaviour
{
    public bool isEnemy = false;
    private KnockbackHandler kb;
    public int HitID = -1;
    public DamageNumberMain DnM = null;
    public DamageReciever dR = null;
    // Start is called before the first frame update
    void Start()
    {
        
        if(gameObject.GetComponent<Transform>().tag == "Enemy")
        {
            isEnemy = true;
            DnM = GameObject.FindGameObjectWithTag("HitDisplayCanv").GetComponent<DamageNumberMain>();
            dR = GetComponent<DamageReciever>();
        }
    }

    public void TakeDamage(float dmg, int hitCount, int _eleType, bool isCrit)
    {
        //do health removal here
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isEnemy)
        {
            if(collision.GetComponent<KBInfoPass>() != null) //check if its a valid hitbox
            {
                var h = collision.GetComponent<KBInfoPass>();
                var d = collision.GetComponent<DamageGiver>();
                if(h.Hit_ID != HitID)
                {
                    //print("hit with " + h.Damage + " Damage");
                    //print("hit with " + h.HitCount + " Hits");
                    //TakeDamage(h.Damage, h.HitCount, h.eleType); instead of this we call the dmg giver script on the attacker
                    d.CallDamage(this);
                    HitID = h.Hit_ID;
                }
            }
        }
        
    }
}
