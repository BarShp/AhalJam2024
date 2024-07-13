using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : BaseSingletonMonoBehaviour<SoundManager>
{
    [SerializeField] SoundAudioClip[] myAudioClips;

    [SerializeField] private List<AudioSource> audioSources;
    
    public enum Sound
    {
        MainMenu,
        Combat,
        Interact
    }

    public void PLaySound(Sound sound)
    {
        var audioClip = myAudioClips.First(soundClips => soundClips.sound == sound).audioClip;
        
        for (var i = 0; i < audioSources.Count; i++)
        {
            var audioSource = audioSources[i];
            if (audioSource.isPlaying) continue;

            audioSource.clip = audioClip;
            audioSource.Play();
            return;
        }

        audioSources[0].clip = audioClip;
        audioSources[0].Play();
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