using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public float DefaultTimescale;
    public bool gTimeStop = false;
    public bool uTimeStop = false;
    [Header("Hit-stop Effect")]
    public float effectTimeToReset = 0;
    public GameObject hitStopEffect;

    void Start()      
    {
        hitStopEffect.SetActive(false); // hitstop effect
    }
    public void UnitSpecificHitStop(GameObject unit, float dur)
    {
        uTimeStop = true;
        StopAllCoroutines();
        StartCoroutine(UnitTimer(dur, unit));
    }

    public void GlobalHitStop(float dur, float slowdownRate)
    {
        hitStopEffect.SetActive(true); // hitstop effect
        StopAllCoroutines();
        Time.timeScale = slowdownRate;
        gTimeStop = true;
        StartCoroutine(GlobalTimer(dur));
    }

    IEnumerator UnitTimer(float dur, GameObject unit)
    {
        Vector2 v = Vector2.zero;
        unit.GetComponent<Animator>().speed = 0; //pauses the animation
        if(unit.GetComponent<HeavyAttHitStop>().isDetachedFromPlayer)
        {
            v = unit.GetComponent<Rigidbody2D>().velocity;
            unit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        yield return new WaitForSeconds(dur);
        unit.GetComponent<Animator>().speed = 1; //unpauses the animation
        if(unit.GetComponent<HeavyAttHitStop>().isDetachedFromPlayer)
        {
            unit.GetComponent<Rigidbody2D>().velocity = v;
        }
        uTimeStop = false;
    }
    IEnumerator GlobalTimer(float dur)
    {
        yield return new WaitForSecondsRealtime(dur);
        Time.timeScale = DefaultTimescale;
        gTimeStop = false;
        hitStopEffect.SetActive(false);
    }
}
