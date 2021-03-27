using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenWipeCD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(screenWipeCooldown());
    }
    IEnumerator screenWipeCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
    }


}
