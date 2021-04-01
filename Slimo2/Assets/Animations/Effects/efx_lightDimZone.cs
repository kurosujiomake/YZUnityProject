using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class efx_lightDimZone : MonoBehaviour
{
    //public GameObject lightZones;
    Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        Debug.Log("found it");
    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(Time.time, 8);
    }

}