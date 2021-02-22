using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffBasic : MonoBehaviour
{
    public float Duration;
    void Start()
    {
        StartCoroutine(LifeTimer());
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(Duration);
        Destroy(gameObject);
    }
}
