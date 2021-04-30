using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecificHitStop : MonoBehaviour
{
    private HeavyAttHitStop hStop = null;
    public Rigidbody2D rb2d = null;
    public float hStopTime = 0;
    public Vector2 originalVel;
    public Animator anim = null;
    public BaseEnemyScript script = null;

    public void CheckforHitStop(Collider2D hBox)
    {
        if (hBox.GetComponentInParent<HeavyAttHitStop>() == null)
        {
            if(hBox.GetComponent<HeavyAttHitStop>() == null)
            {
                hStop = null;
            }
            if(hBox.GetComponent<HeavyAttHitStop>() != null)
            {
                hStop = hBox.GetComponent<HeavyAttHitStop>();
            }
            
        }
        if (hBox.GetComponentInParent<HeavyAttHitStop>() != null)
        {
            hStop = hBox.GetComponentInParent<HeavyAttHitStop>();
        }
        if(hStop != null)
        {
            if(hStop.isHeavyAtkOn)
            {
                StopAllCoroutines();
                StartCoroutine(HitStopTimer());
            }
        }
    }

    IEnumerator HitStopTimer()
    {
        script.HitStopCall(0);
        anim.speed = 0;
        yield return new WaitForSeconds(hStopTime);
        anim.speed = 1;
        script.HitStopCall(1);
        GetComponent<DamageTakeCycles>().StartKBCycle();
        hStop = null;
    }
}
