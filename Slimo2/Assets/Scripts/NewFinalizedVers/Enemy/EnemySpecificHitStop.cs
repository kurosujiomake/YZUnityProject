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

    public void CheckforHitStop(Collider2D hBox)
    {
        if (hBox.GetComponentInParent<HeavyAttHitStop>() == null)
        {
            hStop = null;
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
        print("enemy's own hitstop activated");
        originalVel = rb2d.velocity;
        rb2d.velocity = Vector2.zero;
        anim.speed = 0;
        yield return new WaitForSeconds(hStopTime);
        anim.speed = 1;
        rb2d.velocity = originalVel;
        hStop = null;
    }
}
