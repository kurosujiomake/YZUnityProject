using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTake : MonoBehaviour
{
    public bool isEnemy = false;
    private KnockbackHandler kb;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Transform>().tag == "Enemy")
        {
            isEnemy = true;
            kb = GetComponent<KnockbackHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isEnemy)
        {
            if (collision.GetComponent<Transform>().tag == "LightAttAway")
            {
                print("I got hit with light att with push knockback");
                kb.WhoHitMe(collision.GetComponent<Transform>());
                kb.KnockBacks("Light", true);
            }
            if(collision.GetComponent<Transform>().tag == "UpwardsHit")
            {
                print("I got hit with a knockup attack");
                kb.WhoHitMe(collision.GetComponent<Transform>());
                kb.KnockBacks("Upward", true);
                kb.KnockBacks(2.5f);
            }
        }
        
    }
}
