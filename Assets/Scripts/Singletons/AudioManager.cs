using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    private static AudioManager instance = null;
    public static AudioManager Instance { get { return instance; } }

    AudioSource audioSource;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        ReloadAudioSources();
    }

    public void ReloadAudioSources()
    {
        if (sounds == null)
            return;

        float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        foreach (Sound sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.name = sound.Name;
            source.clip = sound.Clip;
            source.volume = sound.volume * MasterVolume;
            source.pitch = sound.pitch;
            source.mute = sound.mute;
            source.loop = sound.loop;
            source.priority = sound.priority;

            sound.source = source;
        }
    }

    public AudioSource findAudio(string name)
    {
        if (sounds == null)
            return null;

        Sound sound = Array.Find(sounds, s => s.Name == name);
        if (sound != null)
        {
            float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
            sound.source.volume = MasterVolume;
            return sound.source;
        }

        return null;
    }

    public AudioSource findAudio(int id)
    {
        if (sounds == null)
            return null;

        Sound sound = Array.Find(sounds, s => s.Id == id);
        if (sound != null)
        {
            float MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
            sound.source.volume = MasterVolume;
            return sound.source;
        }

        return null;
    }
}

