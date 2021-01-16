using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * This script calls the AudioManager from the player's Animator state to be played by the Audio Listener by calling
 * the AudioManager script from the game object tagged "GameManager". This public string looks for a string of
 * the same name in the inspector component of the AudioManager script and activates the function matching the name,
 * however it is declared.
 * 
 * 1. Uncomment the OnState you want to use.
 * 2. Add this script to the Animator state you want sound to play at.
 * 3. Add a sound in AudioManager Inspector. Ensure the name is correct.
 * 4. Add the sound name in the Animation State Inspector, in the SoundScript component. Ensure the name matches.
 * !! When you change the name of a sound in the AudioManager, you must do so in this script's Inspector !!
 */
public class AudioManagerAnimExit : StateMachineBehaviour
{
    public string soundName;
    public AudioManager audioManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    /*
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        Debug.Log("sound");
        audioManager.PlaySound(soundName);
    }
    */

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    /*
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    */

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        audioManager.PlaySound(soundName);
    }


    // OnStateMove is called right after Animator.OnAnimatorMove()
    /*
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }
    */

    // OnStateIK is called right after Animator.OnAnimatorIK()
    /*
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
    */
}