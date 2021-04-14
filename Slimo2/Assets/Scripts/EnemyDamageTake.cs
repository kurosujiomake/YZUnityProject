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
    public EnemyHP eHP = null;
    public BaseHPSet bHS;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<Transform>().tag == "Enemy")
        {
            isEnemy = true;
            DnM = GameObject.FindGameObjectWithTag("HitDisplayCanv").GetComponent<DamageNumberMain>();
            dR = GetComponent<DamageReciever>();
            eHP = GetComponent<EnemyHP>();
            SetHP();
        }
    }

    void SetHP()
    {
        eHP.SetStartHP(bHS.baseMaxHP * bHS.hpMulti);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isEnemy)
        {
            if(collision.GetComponent<KBInfoPass>() != null) //check if its a valid hitbox
            {
                var h = collision.GetComponent<KBInfoPass>();
                var d = collision.GetComponent<DamageGiver>();
                var c = collision.GetComponent<DamageTransfer>();
                if(h.Hit_ID != HitID) //check that its not continously hittign the enemy
                {
                    if(h.SourceID == 0 || h.SourceID == 3) //0 is player source, 3 is hit everything
                    {
                        HitID = h.Hit_ID; //set hit id so it doesnt get hit by this box again
                        dR.TakeDamage(c.dmgData.ReturnFloatValues("dmg"), c.dmgData.ReturnIntValues("ele"), c.dmgData.ReturnBools("crit"), c.dmgData.ReturnIntValues("hitCount"), h.hType);
                    }
                    
                }
            }
        }
        
    }
}

[System.Serializable]
public class BaseHPSet
{
    public float baseMaxHP;
    public float hpMulti;
}
