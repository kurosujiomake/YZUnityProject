using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    public float DefaultTimescale;
    public bool gTimeStop = false;
    public bool uTimeStop = false;

    public void UnitSpecificHitStop(GameObject unit, float dur)
    {
        uTimeStop = true;
        StopAllCoroutines();
        StartCoroutine(UnitTimer(dur, unit));
    }

    public void GlobalHitStop(float dur, float slowdownRate)
    {
        StopAllCoroutines();
        Time.timeScale = slowdownRate;
        gTimeStop = true;
        StartCoroutine(GlobalTimer(dur));
    }

    IEnumerator UnitTimer(float dur, GameObject unit)
    {
        unit.GetComponent<Animator>().speed = 0; //pauses the animation
        yield return new WaitForSeconds(dur);
        unit.GetComponent<Animator>().speed = 1; //unpauses the animation
        uTimeStop = false;
    }
    IEnumerator GlobalTimer(float dur)
    {
        yield return new WaitForSecondsRealtime(dur);
        Time.timeScale = DefaultTimescale;
        gTimeStop = false;
    }
}
