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
    GameObject levelMenu, paddle;

    [SerializeField]
    Button resumeLevelButton, nextLevelButton, restartLevelButton, mainMenuButton;
    [SerializeField]
    TextMeshProUGUI subMenuTitleTextObject;

    int level;
    bool levelComplete, togglePauseMenu;
    public bool LevelComplete { get { return levelComplete; } }

    float totalStandardBricks, totalBonusBricks;
    float currentStandardBricks, currentBonusBricks;
    int score, bonusScore;
    float timeScale;

    SessionManager sessionManager;
    LevelManager levelManager;
    AudioManager audioManager;

    AudioSource UIButtonClickAudio;
    AudioSource LevelBackgroundAudio;

    void Start()
    {
        if (StandardBrickType == null || BonusBrickType == null)
            throw new MissingReferenceException("StandardBrickType, BonusBrickType");
        if (levelMenu == null || paddle == null)
            throw new MissingReferenceException("levelMenu, paddle");
        if (nextLevelButton == null || restartLevelButton == null || mainMenuButton == null || resumeLevelButton == null || subMenuTitleTextObject == null)
            throw new MissingReferenceException("nextLevelButton, restartLevelButton, mainMenuButton, resumeLevelButton, subMenuTitleTextObject");

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

        levelMenu.SetActive(false);
        paddle.SetActive(true);

        totalStandardBricks = StandardBrickType.transform.childCount;
        currentStandardBricks = totalStandardBricks;

        totalBonusBricks = BonusBrickType.transform.childCount;
        currentBonusBricks = totalBonusBricks;

        score = 0;
        timeScale = Time.timeScale;

        levelComplete = false;
        togglePauseMenu = false;

        resumeLevelButton.onClick.AddListener(ResumeLevelButtonAction);
        nextLevelButton.onClick.AddListener(NextLevelButtonAction);
        restartLevelButton.onClick.AddListener(RestartLevelButtonAction);
        mainMenuButton.onClick.AddListener(MainMenuButtonAction);
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

            sessionManager.SetLevelScore(gameObject, score);

            if (sessionManager.GetLevelStatus(sessionManager.CurrentLevel + 1) == LevelStatus.Locked)
                sessionManager.SetLevelStatus(gameObject, LevelStatus.Unlocked, sessionManager.CurrentLevel + 1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            togglePauseMenu = togglePauseMenu ? false : true;
    }

    void LateUpdate()
    {
        if (togglePauseMenu && !levelMenu.activeSelf)
        {
            Time.timeScale = timeScale / 1000f;
            subMenuTitleTextObject.text = "Pause Menu";
            resumeLevelButton.gameObject.SetActive(true);
            levelMenu.SetActive(true);
            paddle.SetActive(false);
        }
        else if (!togglePauseMenu && levelMenu.activeSelf && resumeLevelButton.gameObject.activeSelf)
        {
            ResumeLevelButtonAction();
        }

        // if levelMenu is active and its not pause menu then no need for other code exec
        if (levelMenu.activeSelf && !togglePauseMenu)
            return;

        // Updating the text gameObjects on the scene
        if (levelTextObject != null)
            levelTextObject.text = "Level: " + level;
        if (scoreTextObject != null)
            scoreTextObject.text = "Score: " + score;

        if (levelComplete)
        {
            subMenuTitleTextObject.text = "Level Complete - " + score.ToString() + "%";

            paddle.SetActive(false);
            levelMenu.SetActive(true);

            // NextLevelButton not required if there is no next level
            if (sessionManager.GetNextLevel() == sessionManager.CurrentLevel)
                nextLevelButton.gameObject.SetActive(false);
            else
                nextLevelButton.gameObject.SetActive(true);
        }
    }

    void ResumeLevelButtonAction()
    {
        resumeLevelButton.gameObject.SetActive(false);
        levelMenu.SetActive(false);
        paddle.SetActive(true);
        togglePauseMenu = false;
        Time.timeScale = timeScale;
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
        Time.timeScale = timeScale;
        levelManager.LoadScene(sessionManager.CurrentLevel);
    }

    void MainMenuButtonAction()
    {
        if (UIButtonClickAudio != null)
            UIButtonClickAudio.Play();
        if (LevelBackgroundAudio != null)
            LevelBackgroundAudio.Pause();
        Time.timeScale = timeScale;
        levelManager.LoadScene(-1);
    }
}
