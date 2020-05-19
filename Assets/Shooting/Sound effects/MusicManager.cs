using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    Music currentMusic;
    public Music[] playList;
    public AudioSource audioSource;
    int modulus;
    float volumeLimit;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (Music music in playList)
        {
            music.source = gameObject.AddComponent<AudioSource>();
            music.source.name = music.name;
            music.source.clip = music.clip;
            music.source.pitch = music.pitch;
            music.source.volume = music.volume;
        }
    }

    private void FixedUpdate()
    {
        if(audioSource.volume < volumeLimit)
        {
            audioSource.volume += 0.5f * Time.fixedDeltaTime;
        }

        NextMusic();
    }

    public void Play(string name)
    {
        Music newMusic = Array.Find(playList, music => music.name == name);

        if (newMusic == null)
            return;

        if (currentMusic != newMusic)
        {

            audioSource.clip = newMusic.clip;
            volumeLimit = newMusic.volume;
            audioSource.volume = newMusic.volume / 2;
            audioSource.pitch = newMusic.pitch;
            audioSource.time = newMusic.setStartTime;
            audioSource.Play();
        }
    }

    private void NextMusic()
    {
        if (audioSource != null)
        {
            if (audioSource.isPlaying == false)
            {
                for (int i = 0; i < playList.Length; i++)
                {
                    if (playList[i].clip == audioSource.clip)
                    {
                        modulus = (i + 1) % playList.Length;
                        audioSource.clip = playList[modulus].clip;
                        audioSource.volume = playList[modulus].volume / 2;
                        audioSource.pitch = playList[modulus].pitch;
                        currentMusic = playList[modulus];
                        audioSource.Play();
                        break;

                    }
                }
            }
        }
    }
}
