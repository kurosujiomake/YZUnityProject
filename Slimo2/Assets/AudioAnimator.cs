using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnimator : MonoBehaviour
{
    public AudioManager audioManager;
    Animator PlayerAnimCont;
    public string soundName;
    public bool canSound;
    void Start()
    {
        canSound = false;
    }

    void Update()
    {
        if (canSound == true)
        {
            audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
            audioManager.PlaySound(soundName);
            Debug.Log("the bool works!");
        }

        else
        {
            canSound = false;
            //Debug.Log("the bool works again.");
        }
    }
}
