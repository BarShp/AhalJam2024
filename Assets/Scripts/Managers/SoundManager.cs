using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : BaseSingletonMonoBehaviour<SoundManager>
{
    [SerializeField] SoundAudioClip[] myAudioClips;

    [SerializeField] private List<AudioSource> audioSources;

    private HashSet<AudioSource> continuousAudioSources = new();
    
    public enum Sound
    {
        MainMenu,
        Combat,
        Interact,
        Door,
        Voices,
        HeartBeat
    }

    public void PLaySound(Sound sound)
    {
        var audioClip = GetAudioSound(sound);
        var audioSource = GetAudioSource();

        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public AudioSource PlayContinuous(Sound sound, bool startAtRandomTime = false)
    {
        var audioClip = GetAudioSound(sound);
        var audioSource = GetAudioSource();

        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.time = Random.Range(0f, audioSource.clip.length);
        audioSource.Play();
        continuousAudioSources.Add(audioSource);

        return audioSource;
    }

    public void SetAudioSourceSpeed(AudioSource audioSource, float speed)
    {
        audioSource.pitch = speed;
    }

    public void StopContinuous(Sound sound)
    {
        var audioClip = GetAudioSound(sound);
        var continuousAudioSource = audioSources.FirstOrDefault(x => x.clip == audioClip);

        if (continuousAudioSource == null) return;
        
        continuousAudioSources.Remove(continuousAudioSource);

        continuousAudioSource.loop = false;
        continuousAudioSource.Stop();
    }

    private AudioClip GetAudioSound(Sound sound)
    {
        return myAudioClips.First(soundClips => soundClips.sound == sound).audioClip;
    }
    

    private AudioSource GetAudioSource()
    {
        for (var i = 0; i < audioSources.Count; i++)
        {
            var audioSource = audioSources[i];
            if (audioSource.isPlaying || continuousAudioSources.Contains(audioSource)) continue;

            return audioSource;
        }

        return audioSources[0];
    }
}

[Serializable]
public class SoundAudioClip
{
    public SoundManager.Sound sound;
    public AudioClip audioClip;
}