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

        if (sounds != null)
        {
            foreach (Sound sound in sounds)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.name = sound.Name;
                source.clip = sound.Clip;
                source.volume = sound.volume;
                source.pitch = sound.pitch;
                source.mute = sound.mute;
                source.loop = sound.loop;
                source.priority = sound.priority;

                sound.source = source;
            }
            audioSource = findAudio(1);
        }
    }

    void Start()
    {
        if (audioSource != null)
            audioSource.Play();
    }

    public AudioSource findAudio(string name)
    {
        if (sounds == null)
            return null;

        Sound sound = Array.Find(sounds, s => s.Name == name);
        if (sound != null)
            return sound.source;

        return null;
    }

    public AudioSource findAudio(int id)
    {
        if (sounds == null)
            return null;

        Sound sound = Array.Find(sounds, s => s.Id == id);
        if (sound != null)
            return sound.source;

        return null;
    }
}

