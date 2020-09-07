using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKBHandler : MonoBehaviour // this script is for all knockback suffered by the player
{
    [SerializeField] private bool isHurt = false;
    [SerializeField] private bool canGoToIdle = false;
    private GroundChecker ground;
    

    // Start is called before the first frame update
    void Start()
    {
        ground = GetComponent<GroundChecker>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canGoToIdle && ground.ReturnGroundCheck())
        {
            isHurt = true;
        }

    }

    //private IEnumerator KB
}
