using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] private AudioData defaultMusic;
    const float MIN_PITCH = 0.9f;
    const float MAX_PITCH = 1.1f;

    protected override void Awake()
    {
        base.Awake();
        this.PlayMusic();
    }

    // Used for UI SFX
    public void PlaySFX(AudioData audioData)
    {
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.volume);
        
    }

    // Used for repeat-play SFX
    public void PlayRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlaySFX(audioData);
    }

    public void PlayRandomSFX(AudioData[] audioData)
    {
        PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }

    public void PlayMusic()
    {
        musicPlayer.clip = this.defaultMusic.audioClip;
        musicPlayer.Play();
    }

    public void PlayMusic(AudioData audioData)
    {
        musicPlayer.clip = audioData.audioClip;
        musicPlayer.Play();
    }
}

[System.Serializable] public class AudioData
{
    public AudioClip audioClip;
    public float volume;
}