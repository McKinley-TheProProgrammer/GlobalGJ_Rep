using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonGlobal<AudioManager>
{
    [SerializeField]
    private List<Sound> sounds;

    private Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();
    void Start()
    {
        foreach (var sound in sounds)
        {
            sound.AudioSource = gameObject.AddComponent<AudioSource>();
            sound.AudioSource.clip = sound.clip;
            sound.AudioSource.loop = sound.loop;
            sound.AudioSource.pitch = sound.pitch;
            sound.AudioSource.volume = sound.volume;
            
            soundDictionary.TryAdd(sound.tagClip, sound);
        }
    }

    public void Play(Sound sound)
    {
        if (!soundDictionary.TryGetValue(sound.tagClip, out sound))
        {
            Debug.LogError($"Sound {sound.tagClip} not found");
            return;
        }
        
        sound.AudioSource.Play();
    }
    
    public void Stop(Sound sound)
    {
        if (!soundDictionary.TryGetValue(sound.tagClip, out sound))
        {
            Debug.LogError($"Sound {sound.tagClip} not found");
            return;
        }
        
        sound.AudioSource.Stop();
    }
}
