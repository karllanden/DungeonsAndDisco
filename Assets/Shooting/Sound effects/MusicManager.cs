using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Music[] musicColletion;
    public AudioSource audioManager;
    Music lastPlayedMusic, currentMusic;
    void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    public void Play(string name)
    {
        Music m = Array.Find(musicColletion, music => music.name == name);
        if (lastPlayedMusic != m)
        {
            lastPlayedMusic = m;
            audioManager.clip = m.clip;
            audioManager.pitch = m.pitch;
            audioManager.volume = m.volume;
            audioManager.Play();
        }


    }
}
