using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTakeCycles : MonoBehaviour
{
    public DamageReciever DR = null;
    public PlayerControllerNew pCN = null;
    public Animator anim = null;
    public Rigidbody2D rb2d = null;
    public KBDatabase kbD = null;
    private int curHitID = -1;
    public float playerKBMulti = 50;
    public bool isInvincible = false;
    public HitStop hStop = null;
    // Start is called before the first frame update
    void Start()
    {
        if(DR == null)
        {
            DR = GetComponent<DamageReciever>();
        }
        if(pCN == null)
        {
            pCN = GetComponent<PlayerControllerNew>();
        }
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        rb2d = GetComponent<Rigidbody2D>();
        hStop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HitStop>();
    }

    //Here is where the player takes damage
    private void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.GetComponent<KBInfoPass>() != null)
        {
            var kb = collider.GetComponent<KBInfoPass>();
            if(kb.SourceID == 1 || kb.SourceID == 3) //player only takes damage from these sources: 1 = enemy, 3 = all
            {
                if(kb.Hit_ID != curHitID && !isInvincible)
                {
                    anim.SetTrigger("GotHit");
                    hStop.GlobalHitStop(1, 0);
                    StopAllCoroutines();
                    StartCoroutine(PKB(kb.curKBNum, kb.enemySource.transform));
                    DR.TakeDamage(kb.Damage, kb.eleType, false, kb.HitCount, kb.hType);
                    curHitID = kb.Hit_ID;
                }
                
            }
        }
    }
    IEnumerator PKB(int KB_ID, Transform enemyPos)
    {
        pCN.SetPState(2);
        StopMovement();
        rb2d.gravityScale = 1;
        PKnockback(kbD.Dir(KB_ID) * playerKBMulti, kbD.Vel(KB_ID) * playerKBMulti, enemyPos);
        yield return new WaitForSeconds(kbD.KBDur(KB_ID));
        rb2d.gravityScale = 5;
        StopMovement();
        if(!GetComponent<Parameters>().m_isDashing && !GetComponent<Parameters>().m_isADashing)
        {
            pCN.SetPState(1);
        }
        
        anim.SetTrigger("BackToIdle");
    }
    private void StopMovement()
    {
        rb2d.velocity = Vector2.zero;
    }
    void PKnockback(float dir, float force, Transform enemyPos)
    {
        float d = 0;
        float f = force * Time.deltaTime;
        if(enemyPos.position.x < transform.position.x) //enemy is to the left side of the player
        {
            d = (dir - 90) * Mathf.Deg2Rad;
        }
        if(enemyPos.position.x > transform.position.x)
        {
            d = dir * Mathf.Deg2Rad;
        }
        Vector2 v = new Vector2(Mathf.Cos(d) * f, Mathf.Sin(d) * f);
        rb2d.velocity = v;
    }
    public void InvincibilityTimer(float dur)
    {
        StopAllCoroutines();
        StartCoroutine(InvinTimer(dur));
    }
    IEnumerator InvinTimer(float dur)
    {
        isInvincible = true;
        yield return new WaitForSeconds(dur);
        isInvincible = false;
    }
}
