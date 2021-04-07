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
    public float playerKBMulti;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.GetComponent<KBInfoPass>() != null)
        {
            var kb = collider.GetComponent<KBInfoPass>();
            if(kb.SourceID == 1 || kb.SourceID == 3) //player only takes damage from these sources: 1 = enemy, 3 = all
            {
                if(kb.Hit_ID != curHitID)
                {
                    anim.SetTrigger("GotHit");
                    StopAllCoroutines();
                    StartCoroutine(PKB(kb.curKBNum));
                }
                
            }
        }
    }
    IEnumerator PKB(int KB_ID)
    {
        pCN.SetPState(2);
        StopMovement();
        rb2d.gravityScale = 1;
        PKnockback(kbD.Dir(KB_ID) * playerKBMulti, kbD.Vel(KB_ID) * playerKBMulti);
        yield return new WaitForSeconds(kbD.KBDur(KB_ID));
        rb2d.gravityScale = 5;
        StopMovement();
        pCN.SetPState(1);
        anim.SetTrigger("BackToIdle");
    }
    private void StopMovement()
    {
        rb2d.velocity = Vector2.zero;
    }
    void PKnockback(float dir, float force)
    {
        float d = dir * Mathf.Deg2Rad;
        float f = force * Time.deltaTime;
        
        Vector2 v = new Vector2(Mathf.Cos(d) * f, Mathf.Sin(d) * f);
        rb2d.velocity = v;
    }
}
