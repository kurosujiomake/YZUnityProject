using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulseEffect : MonoBehaviour
{
    public float lifeTime = 0;
    private float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
