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
        foreach (Music music in musicColletion)
        {
            music.source = gameObject.AddComponent<AudioSource>();
            music.source.name = music.name;
            music.source.clip = music.clip;
            music.source.pitch = music.pitch;
            music.source.volume = music.volume;
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Music m = Array.Find(musicColletion, music => music.name == name);
        if (currentMusic != m)
        {
            currentMusic = m;
            audioManager = m.source;
            audioManager.Play();
        }


    }

    //public void Stop(string name)
    //{
    //    Music m = Array.Find(musicColletion, music => music.name == name);
    //    audioManager = m.source;
    //    audioManager.Stop();
    //}
}
