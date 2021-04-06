using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class efx_lightDimZone : MonoBehaviour
{
    Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(Time.time, 8);
    }

}