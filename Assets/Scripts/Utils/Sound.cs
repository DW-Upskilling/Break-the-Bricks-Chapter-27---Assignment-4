using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] int id; // unique integer value of the sound effect
    public int Id { get { return id; } }

    [SerializeField] string name; // Name of the sound effect
    public string Name { get { return name; } }

    [SerializeField] AudioClip clip; // Audio clip for the sound effect
    public AudioClip Clip { get { return clip; } }

    [Range(0f, 1f)]
    public float volume = 1f; // Volume of the sound effect (0 to 1)
    [Range(0.1f, 3f)]
    public float pitch = 1f; // Pitch of the sound effect (0.1 to 3)
    [Range(0, 256)]
    public int priority = 32; // Priority of the sound effect (0 to 256)(high to low)

    public bool loop = false; // Indicates if the sound effect should loop
    public bool mute = true; // Indicates if the sound effect should be muted

    [HideInInspector] public AudioSource source; // Reference to the AudioSource component for this sound effect
}