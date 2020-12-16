using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //particleLauncher.Emit(0);
    }

    public void PrintEvent(string s)
    {
        Debug.Log("Particles!");
    }

}
