using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject LockedButtonPrefab, UnlockedButtonPrefab, CompletedButtonPrefab;
    [SerializeField]
    private GameObject MenuSection, LevelsSection;

    [SerializeField]
    private Button StartButton, QuitButton;

    void Start()
    {
        if (StartButton != null)
            StartButton.onClick.AddListener(StartButtonAction);
        if (QuitButton != null)
            QuitButton.onClick.AddListener(QuitButtonAction);
    }

    void StartButtonAction()
    {
        if (SessionManager.Instance != null)
            SessionManager.Instance.Intiated = true;

        LevelsSectionReload();
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
        }

        // Hide MenuSection
        if (MenuSection != null)
            MenuSection.SetActive(false);
        // Show LevelsSection
        if (LevelsSection != null)
            LevelsSection.SetActive(true);
    }
}
