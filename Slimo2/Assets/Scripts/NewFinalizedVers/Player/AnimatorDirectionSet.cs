using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDirectionSet : MonoBehaviour
{
    public Animator animator = null;
    public PlayerControlManager pCM = null;
    public PlayerControllerNew pCN = null;
    void Awake()
    {
        animator = GetComponent<Animator>();
        pCN = GetComponent<PlayerControllerNew>();
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (pCM.GetDirectionL()) //set the int in the animator so it knows which aerial to trigger
        {
            case "n":
                animator.SetInteger("AirDirection", 0);
                break;
            case "r":
                if (pCN.facingRight)
                    animator.SetInteger("AirDirection", 1); //if facing right then right input is forward
                if (!pCN.facingRight)
                    animator.SetInteger("AirDirection", 2); //if not then left input is forward
                break;
            case "l":
                if (pCN.facingRight)
                    animator.SetInteger("AirDirection", 2); //see above
                if (!pCN.facingRight)
                    animator.SetInteger("AirDirection", 1);
                break;
            case "u":
                animator.SetInteger("AirDirection", 3);
                break;
            case "d":
                animator.SetInteger("AirDirection", 4);
                break;
            case "ur":
                if (pCN.facingRight)
                    animator.SetInteger("AirDirection", 5);
                if (!pCN.facingRight)
                    animator.SetInteger("AirDirection", 6);
                break;
            case "ul":
                animator.SetInteger("AirDirection", 6);
                break;
            case "dr":
                animator.SetInteger("AirDirection", 7);
                break;
            case "dl":
                animator.SetInteger("AirDirection", 8);
                break;
        }
    }
}
