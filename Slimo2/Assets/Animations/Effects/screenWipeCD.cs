using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenWipeCD : MonoBehaviour
{
    void Start()
    {
        Invoke("DeactivateThisObject", 1.0f);
        //StartCoroutine(screenWipeCooldown());
    }

    /*IEnumerator screenWipeCooldown()
    {
        yield return new WaitForSecondsRealtime(1);
        gameObject.SetActive(false);
    }
    */
    private void DeactivateThisObject()
    {
        gameObject.SetActive(false);
    }
}