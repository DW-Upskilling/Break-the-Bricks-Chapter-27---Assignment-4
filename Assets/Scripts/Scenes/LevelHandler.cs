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
    AudioManager audioManager;

    AudioSource UIButtonClickAudio;
    AudioSource LevelBackgroundAudio;

    void Start()
    {
        if (StandardBrickType == null || BonusBrickType == null)
            throw new MissingReferenceException("StandardBrickType, BonusBrickType");
        if (levelCompleteMenu == null || paddle == null)
            throw new MissingReferenceException("levelCompleteMenu, paddle");
        if (nextLevelButton == null || restartLevelButton == null || mainMenuButton == null)
            throw new MissingReferenceException("nextLevelButton, restartLevelButton, mainMenuButton");

        sessionManager = SessionManager.Instance;
        levelManager = LevelManager.Instance;
        audioManager = AudioManager.Instance;
        if (sessionManager == null || levelManager == null || audioManager == null)
            throw new MissingReferenceException("SessionManager, LevelManager, AudioManager");

        LevelBackgroundAudio = audioManager.findAudio(2);
        UIButtonClickAudio = audioManager.findAudio("UIButtonClick");

        if (LevelBackgroundAudio != null)
            LevelBackgroundAudio.Play();

        level = sessionManager.CurrentLevel;

        levelCompleteMenu.SetActive(false);
        paddle.SetActive(true);

        totalStandardBricks = StandardBrickType.transform.childCount;
        currentStandardBricks = totalStandardBricks;

        totalBonusBricks = BonusBrickType.transform.childCount;
        currentBonusBricks = totalBonusBricks;

        score = 0;

        levelComplete = false;
    }

    void Update()
    {
        if (levelComplete)
            return;

        currentStandardBricks = StandardBrickType.transform.childCount;
        currentBonusBricks = BonusBrickType.transform.childCount;

        // Calculating Score
        // Priority: 80% Standard Bricks, 20% Bonus Bricks
        score = Mathf.RoundToInt(
            Mathf.Clamp01(
                (totalStandardBricks - currentStandardBricks) / totalStandardBricks
            ) * 80f + Mathf.Clamp01(
                (totalBonusBricks - currentBonusBricks) / totalBonusBricks
            ) * 20f
        );

        // If all standardBricks were cleared consider level complete
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
        // if levelCompleteMenu is active then no need for other code exec
        if (levelCompleteMenu.activeSelf == true)
            return;

        // Updating the text gameObjects on the scene
        if (levelTextObject != null)
            levelTextObject.text = "Level: " + level;
        if (scoreTextObject != null)
            scoreTextObject.text = "Score: " + score;

        if (levelComplete)
        {
            paddle.SetActive(false);
            levelCompleteMenu.SetActive(true);

            // NextLevelButton not required if there is no next level
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
        if (UIButtonClickAudio != null)
            UIButtonClickAudio.Play();
        if (LevelBackgroundAudio != null)
            LevelBackgroundAudio.Pause();
        sessionManager.SetCurrentLevel(gameObject, sessionManager.GetNextLevel());
        levelManager.LoadScene(sessionManager.CurrentLevel);
    }

    void RestartLevelButtonAction()
    {
        if (UIButtonClickAudio != null)
            UIButtonClickAudio.Play();
        if (LevelBackgroundAudio != null)
            LevelBackgroundAudio.Pause();
        levelManager.LoadScene(sessionManager.CurrentLevel);
    }

    void MainMenuButtonAction()
    {
        if (UIButtonClickAudio != null)
            UIButtonClickAudio.Play();
        if (LevelBackgroundAudio != null)
            LevelBackgroundAudio.Pause();
        levelManager.LoadScene(-1);
    }
}
