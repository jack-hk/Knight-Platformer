using System;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class AudioController : MonoBehaviour
{
    // --------------Data-------------- 
    private static AudioController _instance;
    public Audio[] sounds;
    private static Dictionary<string, float> soundTimerDictionary;

    public static AudioController instance
    {
        get
        {
            return _instance;
        }
    }

    // --------------Built-In------------ 

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        soundTimerDictionary = new Dictionary<string, float>();

        foreach (Audio sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.isLoop;

            if (sound.hasCooldown)
            {
                Debug.Log(sound.name);
                soundTimerDictionary[sound.name] = 0f;
            }
        }
    }

    private void Start()
    {
        // Add this part after having a theme song
        // Play('Theme');
    }

    // --------------Functions-------------- 

    public void Play(string name)
    {
        Audio sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " could not be loaded");
            return;
        }

        if (!CanPlaySound(sound)) return;

        sound.source.Play();
    }

    public void Stop(string name)
    {
        Audio sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " could not be loaded");
            return;
        }

        sound.source.Stop();
    }

    private static bool CanPlaySound(Audio sound)
    {
        if (soundTimerDictionary.ContainsKey(sound.name))
        {
            float lastTimePlayed = soundTimerDictionary[sound.name];

            if (lastTimePlayed + sound.clip.length < Time.time)
            {
                soundTimerDictionary[sound.name] = Time.time;
                return true;
            }
            return false;
        }
        return true;
    }
}
