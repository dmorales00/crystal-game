using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource SFXAudioSource;
    public AudioSource MusicAudioSource;
    public List<AudioClip> allSFX;
    public List<AudioClip> allMusic;

    private void Awake()
    {
        if(!GlobalVariables.audioExists)
        {
            instance = this;
            GlobalVariables.audioExists=true;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(SFXAudioSource.gameObject);
            DontDestroyOnLoad(MusicAudioSource.gameObject);
        }
        else
        {
            Destroy(gameObject);
            DontDestroyOnLoad(SFXAudioSource.gameObject);
            DontDestroyOnLoad(MusicAudioSource.gameObject);
        }
    }

    public void StartSFX(AudioClip sfx)
    {
        SFXAudioSource.clip = sfx;
        SFXAudioSource.Play();
    }

    public void StartMusic(AudioClip music)
    {
        MusicAudioSource.clip = music;
        MusicAudioSource.PlayOneShot(music,0.5f);
    }
}
