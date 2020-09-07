using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pStates : MonoBehaviour
{
    [SerializeField] private bool isGrounded = false;
    private GroundChecker gCheck;
    public enum PlayerState { Idle, Attack, Move, Jump, KnockDown, NoInput};
    [SerializeField] private PlayerState pState;
    // Start is called before the first frame update
    void Start()
    {
        gCheck = GetComponent<GroundChecker>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = gCheck.ReturnGroundCheck(); // gets the ground state, passes this to others
        GetComponent<Animator>().SetBool("OnGround", isGrounded); //one of the few places a script directly talks to animator, lets animator know if player is grounded
    }

    public PlayerState returnState() 
    {
        return pState;
    }

    public bool ReturnGround()
    {
        return isGrounded;
    }
    public void SetPState(string state)
    {
        switch(state)
        {
            case "idle":
                pState = PlayerState.Idle;
                break;
            case "attack":
                pState = PlayerState.Attack;
                break;
            case "move":
                pState = PlayerState.Move;
                break;
            case "noInp":
                pState = PlayerState.NoInput;
                break;
            case "jump":
                pState = PlayerState.Jump;
                break;
        }
    }
}
