using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGM : MonoBehaviour
{
    public AudioManager audioManager;
    public string soundName;
    public bool canAudio01;
    private bool m_canAudio01;
    public Animator playerAnimator;

    public void updateAnimateBool()
    {
        if (m_canAudio01 != canAudio01)
        {
            playerAnimator.SetBool("canSound", canAudio01);
            m_canAudio01 = canAudio01;
            audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
            audioManager.PlaySound(soundName);
        }
    }
}