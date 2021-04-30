using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDirectionSet : MonoBehaviour
{
    public Animator animator = null;
    public InputSystemShell pCM = null;
    public PlayerControllerNew pCN = null;
    void Awake()
    {
        animator = GetComponent<Animator>();
        pCN = GetComponent<PlayerControllerNew>();
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<InputSystemShell>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (pCM.GetDirectionL()) //set the int in the animator so it knows which aerial to trigger
        {
            case "n": //neutral
                animator.SetInteger("AirDirection", 0);
                break;
            case "r": //forwards/right
                if (pCN.facingRight)
                    animator.SetInteger("AirDirection", 1); //if facing right then right input is forward
                if (!pCN.facingRight)
                    animator.SetInteger("AirDirection", 2); //if not then left input is forward
                break;
            case "l": //backwards/left
                if (pCN.facingRight)
                    animator.SetInteger("AirDirection", 2); //see above
                if (!pCN.facingRight)
                    animator.SetInteger("AirDirection", 1);
                break;
            case "u": //up
                animator.SetInteger("AirDirection", 3);
                break;
            case "d": //down
                animator.SetInteger("AirDirection", 4);
                break;
            case "ur": //up forwards/up right
                if (pCN.facingRight)
                    animator.SetInteger("AirDirection", 5);
                if (!pCN.facingRight)
                    animator.SetInteger("AirDirection", 6);
                break;
            case "ul": //up backwards/up left
                if (pCN.facingRight)
                    animator.SetInteger("AirDirection", 6);
                if (!pCN.facingRight)
                    animator.SetInteger("AirDirection", 5);
                break;
            case "dr": //down forwards/down right
                if (pCN.facingRight)
                    animator.SetInteger("AirDirection", 7);
                if (!pCN.facingRight)
                    animator.SetInteger("AirDirection", 8);
                break;
            case "dl": //down backwards/down left
                if (pCN.facingRight)
                    animator.SetInteger("AirDirection", 8);
                if (!pCN.facingRight)
                    animator.SetInteger("AirDirection", 7);
                break;
        }
    }
}
