using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject LockedButtonPrefab, UnlockedButtonPrefab, CompletedButtonPrefab;
    [SerializeField]
    private GameObject MenuSection, LevelsSection;

    [SerializeField]
    private Button StartButton, QuitButton, BackButton, SettingsButton;

    void Start()
    {
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
        if (SessionManager.Instance == null)
            return;
        SessionManager.Instance.ShowMainMenu = false;
        LevelsSectionReload();
        ToggleMainMenu();
    }
    void SettingsButtonAction()
    {
        // TODO: Render Settings Scene
    }
    void BackButtonAction()
    {
        if (SessionManager.Instance == null)
            return;

        SessionManager.Instance.ShowMainMenu = true;
        ToggleMainMenu();
    }
    void QuitButtonAction()
    {
        UnityEngine.Application.Quit();
    }

    void LevelsSectionReload()
    {
        // Check if the SessionManager Singleton is available
        SessionManager session = SessionManager.Instance;
        if (session == null)
            return;

        // Clear the LevelsSection template to refresh the levels
        Transform transform = LevelsSection.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // Add the levels with latest status into the LevelsSection
        for (int i = 0; i < Constants.TotalLevels; i++)
        {
            LevelStatus levelStatus = session.GetLevelStatus(i + 1);
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

            GameObject childObject = levelObject.transform.GetChild(0).gameObject;
            if (childObject.GetComponent<TextMeshProUGUI>() != null)
            {
                childObject.GetComponent<TextMeshProUGUI>().text = "Level-" + (i + 1);
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
