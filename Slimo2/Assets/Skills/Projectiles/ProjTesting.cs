using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjTesting : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    private float timer;
    public SpawnDatabase sDB;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            print(sDB.ReturnBSpeed(1));
        }
    }
}
