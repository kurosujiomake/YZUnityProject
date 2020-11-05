using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewButtonTesting : MonoBehaviour
{
    public PlayerControlManager ct;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ct.GetKeyDown("Atk1"))
        {
            Debug.Log("Atk1 pressed");
        }

    }
}
