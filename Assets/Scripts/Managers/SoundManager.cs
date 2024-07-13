using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : BaseSingletonMonoBehaviour<SoundManager>
{
    private List<AudioSource> currentlyRunningSounds = new List<AudioSource>();
    public enum Sound
    {
        MainMenu,
        Combat,
    }


    [SerializeField] SoundAudioClip[] myAudioClips;

    private AudioSource createAudioGameObject()
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        return audioSource;
    }

    public void PlayOneShot(Sound sound)
    {
        AudioSource audioSource = createAudioGameObject();
        audioSource.volume = audioSource.volume / 2;
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    public void PlayOngoingMusic(Sound sound)
    {
        AudioSource audioSource = createAudioGameObject();
        audioSource.loop = true;
        audioSource.volume = audioSource.volume / 2;
        audioSource.PlayOneShot(GetAudioClip(sound));
        currentlyRunningSounds.Add(audioSource);
    }

    public void ClearSounds()
    {
        if (currentlyRunningSounds.Count != 0)
        {
            foreach (AudioSource audioSource in currentlyRunningSounds)
            {
                print(audioSource.name);
                audioSource.Stop();
            }
        }
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in myAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.Log("Sound" + sound + " not found!");
        return null;
    }
}

[Serializable]
public class SoundAudioClip
{
    public SoundManager.Sound sound;
    public AudioClip audioClip;
}