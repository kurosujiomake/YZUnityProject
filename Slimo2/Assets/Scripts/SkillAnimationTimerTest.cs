using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAnimationTimerTest : MonoBehaviour
{
    public float timer;
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(start)
        {
            timer += Time.deltaTime;
        }

    }
}
