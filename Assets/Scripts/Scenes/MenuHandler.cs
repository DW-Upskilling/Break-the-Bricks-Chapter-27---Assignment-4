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

    void Start()
    {
        sessionManager = SessionManager.Instance;
        levelManager = LevelManager.Instance;
        if (sessionManager == null || levelManager == null)
            throw new MissingReferenceException("SessionManager, LevelManager");

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
        sessionManager.ShowMainMenu = false;
        LevelsSectionReload();
        ToggleMainMenu();
    }
    void SettingsButtonAction()
    {
        SceneManager.LoadScene(settingsSceneBuildIndex);
    }
    void BackButtonAction()
    {
        sessionManager.ShowMainMenu = true;
        ToggleMainMenu();
    }
    void QuitButtonAction()
    {
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
            LevelStatus levelStatus = sessionManager.GetLevelStatus(i + 1);
            GameObject levelObject = null;
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
                    break;
            }
            if (levelObject == null || levelObject.transform.childCount < 1)
                continue;

            int levelValue = i + 1;
            levelObject.name = "Level " + levelValue;

            levelObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                sessionManager.SetCurrentLevel(gameObject, levelValue);
                levelManager.LoadScene(levelValue);
            });

            GameObject childObject = levelObject.transform.GetChild(0).gameObject;
            if (childObject.GetComponent<TextMeshProUGUI>() != null)
            {
                childObject.GetComponent<TextMeshProUGUI>().text = "Level-" + levelValue;
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
