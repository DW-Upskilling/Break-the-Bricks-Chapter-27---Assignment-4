using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI levelTextObject, scoreTextObject;

    [SerializeField]
    GameObject StandardBrickType, BonusBrickType;

    [SerializeField]
    GameObject levelCompleteMenu, paddle;

    [SerializeField]
    Button nextLevelButton, restartLevelButton, mainMenuButton;

    int level;
    bool levelComplete;
    public bool LevelComplete { get { return levelComplete; } }

    float totalStandardBricks, totalBonusBricks;
    float currentStandardBricks, currentBonusBricks;
    int score, bonusScore;

    SessionManager sessionManager;
    LevelManager levelManager;

    void Awake()
    {
        if (StandardBrickType == null || BonusBrickType == null)
            throw new MissingReferenceException("StandardBrickType, BonusBrickType");
        if (levelCompleteMenu == null || paddle == null)
            throw new MissingReferenceException("levelCompleteMenu, paddle");
        if (nextLevelButton == null || restartLevelButton == null || mainMenuButton == null)
            throw new MissingReferenceException("nextLevelButton, restartLevelButton, mainMenuButton");

        sessionManager = SessionManager.Instance;
        levelManager = LevelManager.Instance;
        if (sessionManager == null || levelManager == null)
            throw new MissingReferenceException("SessionManager, LevelManager");

        level = sessionManager.CurrentLevel;

        levelCompleteMenu.SetActive(false);
        paddle.SetActive(true);

        totalStandardBricks = StandardBrickType.transform.childCount;
        currentStandardBricks = totalStandardBricks;

        totalBonusBricks = BonusBrickType.transform.childCount;
        currentBonusBricks = totalBonusBricks;

        score = 0;

        levelComplete = false;

        Debug.Log("Level: " + level);
    }

    void Update()
    {
        if (levelComplete)
            return;

        currentStandardBricks = StandardBrickType.transform.childCount;
        currentBonusBricks = BonusBrickType.transform.childCount;

        score = Mathf.RoundToInt(
            Mathf.Clamp01(
                (totalStandardBricks - currentStandardBricks) / totalStandardBricks
            ) * 80f + Mathf.Clamp01(
                (totalBonusBricks - currentBonusBricks) / totalBonusBricks
            ) * 20f
        );

        if (currentStandardBricks == 0)
        {
            levelComplete = true;
            sessionManager.SetLevelStatus(gameObject, LevelStatus.Completed);

            if (sessionManager.GetLevelStatus(sessionManager.CurrentLevel + 1) == LevelStatus.Locked)
                sessionManager.SetLevelStatus(gameObject, LevelStatus.Unlocked, sessionManager.CurrentLevel + 1);
        }
    }

    void LateUpdate()
    {
        if (levelCompleteMenu.activeSelf == true)
            return;

        if (levelTextObject != null)
            levelTextObject.text = "Level: " + level;
        if (scoreTextObject != null)
            scoreTextObject.text = "Score: " + score;

        if (levelComplete)
        {
            paddle.SetActive(false);
            levelCompleteMenu.SetActive(true);

            if (sessionManager.GetNextLevel() == sessionManager.CurrentLevel)
                nextLevelButton.gameObject.SetActive(false);
            else
                nextLevelButton.onClick.AddListener(NextLevelButtonAction);

            restartLevelButton.onClick.AddListener(RestartLevelButtonAction);
            mainMenuButton.onClick.AddListener(MainMenuButtonAction);
        }
    }

    void NextLevelButtonAction()
    {
        sessionManager.SetCurrentLevel(gameObject, sessionManager.GetNextLevel());
        levelManager.LoadScene(sessionManager.CurrentLevel);
    }

    void RestartLevelButtonAction()
    {
        levelManager.LoadScene(sessionManager.CurrentLevel);
    }

    void MainMenuButtonAction()
    {
        levelManager.LoadScene(-1);
    }
}
