﻿using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;
    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play ()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]
    Sound[] sounds;

    public PlayerControlManager playerControlManager;

    void Awake ()
    {
        playerControlManager = GetComponent<PlayerControlManager>();
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene.");
        }

        else
        {
            instance = this;
        }
    }

    void Start ()
    {
        for(int i = 0; i <sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());

        }

    }

    void Update ()
    {
        //PlayerControlManager playerControlManager = GetComponent<PlayerControlManager>;
        if (playerControlManager)
        {
            if (playerControlManager.Jump_held == true)
            {
                Debug.Log("It works 3dwadadawdadwadwadada!");
            }
        }

        if (GetComponent<PlayerControlManager>())
        {
            Debug.Log("It works!");
            PlaySound("_source");
            if (GetComponent<PlayerControlManager>().Jump_held == true)
            {
                Debug.Log("It works 2dwadadwadwadwadwadwa!");
            }

           
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        if (gameObject.GetComponent<PlayerControlManager>())
        {
            Debug.Log("It works!");
            PlaySound("_source");
            //if (gameObject.GetComponent<PlayerControlManager>().)
        }

        // no sound with name
        Debug.Log("AudioManager : Sound not found in sounds list: " + _name);
    }
}