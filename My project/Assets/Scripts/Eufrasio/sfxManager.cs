using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class sfxManager : MonoBehaviour
{

    public Sound[] sounds;


    float randomPitch;
    // Start is called before the first frame update
    void Awake()
    {
       
       
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.playOnAwake = false;

            s.startPitch = s.pitch;
            s.startVolume = s.volume;

        }
    }

    

    private void Update()
    {
        randomPitch = UnityEngine.Random.Range(-0.05f, 0.05f); 
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s.randomPitch)
        s.source.pitch = s.startPitch + randomPitch;
        else
        s.source.pitch = s.startPitch;

        s.source.volume = s.startVolume + randomPitch;

        s.source.Play();
    }
}

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public float startPitch;
    [HideInInspector]

    public float startVolume;

    public bool randomPitch;

    [HideInInspector]
    public AudioSource source;
}

