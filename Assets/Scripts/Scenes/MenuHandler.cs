using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{

    [SerializeField]
    GameObject LockedButtonPrefab, UnlockedButtonPrefab, CompletedButtonPrefab;
    [SerializeField]
    GameObject MenuSection, LevelsSection;

    [SerializeField]
    int settingsSceneBuildIndex;

    [SerializeField]
    Button StartButton, QuitButton, BackButton, SettingsButton;

    SessionManager sessionManager;
    LevelManager levelManager;
    AudioManager audioManager;

    AudioSource UIButtonClickAudio;
    AudioSource MainMenuBackgroundAudio;

    void Start()
    {
        sessionManager = SessionManager.Instance;
        levelManager = LevelManager.Instance;
        audioManager = AudioManager.Instance;
        if (sessionManager == null || levelManager == null || audioManager == null)
            throw new MissingReferenceException("SessionManager, LevelManager, AudioManager");

        MainMenuBackgroundAudio = audioManager.findAudio(1);
        UIButtonClickAudio = audioManager.findAudio("UIButtonClick");

        if (MainMenuBackgroundAudio != null)
            MainMenuBackgroundAudio.Play();

        if (StartButton != null)
            StartButton.onClick.AddListener(StartButtonAction);
        if (QuitButton != null)
            QuitButton.onClick.AddListener(QuitButtonAction);
        if (BackButton != null)
            BackButton.onClick.AddListener(BackButtonAction);
        if (SettingsButton != null)
            SettingsButton.onClick.AddListener(SettingsButtonAction);
    }

    void StartButtonAction()
    {
        if (UIButtonClickAudio != null)
            UIButtonClickAudio.Play();
        sessionManager.ShowMainMenu = false;
        LevelsSectionReload();
        ToggleMainMenu();
    }
    void SettingsButtonAction()
    {
        if (UIButtonClickAudio != null)
            UIButtonClickAudio.Play();
        SceneManager.LoadScene(settingsSceneBuildIndex);
    }
    void BackButtonAction()
    {
        if (UIButtonClickAudio != null)
            UIButtonClickAudio.Play();
        sessionManager.ShowMainMenu = true;
        ToggleMainMenu();
    }
    void QuitButtonAction()
    {
        if (UIButtonClickAudio != null)
            UIButtonClickAudio.Play();
        UnityEngine.Application.Quit();
    }

    void LevelsSectionReload()
    {
        // Clear the LevelsSection template to refresh the levels
        Transform transform = LevelsSection.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // Add the levels with latest status into the LevelsSection
        for (int i = 0; i < Constants.TotalLevels; i++)
        {
            int levelValue = i + 1;
            LevelStatus levelStatus = sessionManager.GetLevelStatus(levelValue);
            GameObject levelObject = null;
            float score = -1;

            switch (levelStatus)
            {
                case LevelStatus.Locked:
                    levelObject = Instantiate(LockedButtonPrefab, LevelsSection.transform);
                    break;
                case LevelStatus.Unlocked:
                    levelObject = Instantiate(UnlockedButtonPrefab, LevelsSection.transform);
                    break;
                case LevelStatus.Completed:
                    levelObject = Instantiate(CompletedButtonPrefab, LevelsSection.transform);
                    score = sessionManager.GetLevelScore(levelValue);
                    break;
            }
            if (levelObject == null || levelObject.transform.childCount < 1)
                continue;

            levelObject.name = "Level " + levelValue;

            levelObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (UIButtonClickAudio != null)
                    UIButtonClickAudio.Play();
                if (MainMenuBackgroundAudio != null)
                    MainMenuBackgroundAudio.Pause();
                sessionManager.SetCurrentLevel(gameObject, levelValue);
                levelManager.LoadScene(levelValue);
            });

            GameObject childObject = levelObject.transform.GetChild(0).gameObject;
            if (childObject.GetComponent<TextMeshProUGUI>() != null)
            {
                childObject.GetComponent<TextMeshProUGUI>().text = "Level-" + levelValue + (score == -1 ? "" : ("\n" + score.ToString() + "%"));
            }
        }
    }

    void ToggleMainMenu()
    {
        if (SessionManager.Instance == null)
            return;
        if (SessionManager.Instance.ShowMainMenu == false)
        {
            // Show BackButton
            if (BackButton != null)
                BackButton.gameObject.SetActive(true);
            // Hide MenuSection
            if (MenuSection != null)
                MenuSection.SetActive(false);
            // Show LevelsSection
            if (LevelsSection != null)
                LevelsSection.SetActive(true);
        }
        else if (SessionManager.Instance.ShowMainMenu == true)
        {
            // Hide BackButton
            if (BackButton != null)
                BackButton.gameObject.SetActive(false);
            // Show MenuSection
            if (MenuSection != null)
                MenuSection.SetActive(true);
            // Hide LevelsSection
            if (LevelsSection != null)
                LevelsSection.SetActive(false);
        }
    }
}
