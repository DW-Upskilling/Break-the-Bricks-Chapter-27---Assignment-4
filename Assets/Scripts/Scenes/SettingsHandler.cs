using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsHandler : MonoBehaviour
{

    [SerializeField]
    Button SaveButton, BackButton;

    [SerializeField]
    Slider MasterVolumeSlider;

    SessionManager sessionManager;
    LevelManager levelManager;
    AudioManager audioManager;

    AudioSource UIButtonClickAudio;

    void Start()
    {
        sessionManager = SessionManager.Instance;
        levelManager = LevelManager.Instance;
        audioManager = AudioManager.Instance;
        if (sessionManager == null || levelManager == null || audioManager == null)
            throw new MissingReferenceException("SessionManager, LevelManager, AudioManager");

        UIButtonClickAudio = audioManager.findAudio("UIButtonClick");

        if (SaveButton == null || BackButton == null)
            throw new MissingReferenceException("SaveButton, BackButton");

        SaveButton.onClick.AddListener(SaveButtonAction);
        BackButton.onClick.AddListener(BackButtonAction);

        if (MasterVolumeSlider == null)
            throw new MissingReferenceException("MasterVolume");

        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
    }

    void SaveButtonAction()
    {
        if (UIButtonClickAudio != null)
            UIButtonClickAudio.Play();

        PlayerPrefs.SetFloat("MasterVolume", MasterVolumeSlider.value);
    }

    void BackButtonAction()
    {
        if (UIButtonClickAudio != null)
            UIButtonClickAudio.Play();
        levelManager.LoadScene(-1);
    }
}
