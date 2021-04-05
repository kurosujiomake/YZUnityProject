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
                print("Player got hit");
                pCN.SetPState(0);
                pCN.SetPState(2);
                anim.SetTrigger("GotHit");
            }
        }
    }
    IEnumerator PKB(int KB_ID)
    {
        StopMovement();
        PKnockback(kbD.Dir(KB_ID), kbD.Vel(KB_ID));
        yield return new WaitForSeconds(kbD.KBDur(KB_ID));
        StopMovement();
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
