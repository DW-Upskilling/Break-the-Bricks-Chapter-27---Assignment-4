using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] string name; // Name of the sound effect

    [SerializeField] AudioClip clip; // Audio clip for the sound effect
    [Range(0f, 1f)][SerializeField] float volume = 0f; // Volume of the sound effect (0 to 1)
    [Range(.1f, 3f)][SerializeField] float pitch = 0.1f; // Pitch of the sound effect (0.1 to 3)
    [Range(0, 256)][SerializeField] int priority = 0; // Priority of the sound effect (0 to 256)

    [SerializeField] bool loop = false; // Indicates if the sound effect should loop
    [SerializeField] bool mute = true; // Indicates if the sound effect should be muted

    [SerializeField] AudioSource source; // Reference to the AudioSource component for this sound effect
}