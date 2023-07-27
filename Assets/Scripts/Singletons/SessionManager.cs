using UnityEngine;

public class SessionManager : MonoBehaviour
{
    private static SessionManager instance = null;
    public static SessionManager Instance { get { return instance; } }

    private bool showMainMenu = false;
    public bool ShowMainMenu { get { return showMainMenu; } set { showMainMenu = value; } }

    int currentLevel = 0;
    public int CurrentLevel { get { return currentLevel; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetNextLevel()
    {
        if (GetLevelStatus(currentLevel + 1) == LevelStatus.Locked || Constants.TotalLevels <= currentLevel)
            return currentLevel;

        return currentLevel + 1;
    }

    public void SetCurrentLevel(GameObject _gameObject, int level)
    {
        if (GetLevelStatus(level) == LevelStatus.Locked)
            return;

        if (_gameObject.GetComponent<MenuHandler>() != null)
        {
            currentLevel = level;
        }
        else if (_gameObject.GetComponent<LevelHandler>() != null)
        {
            currentLevel = level;
        }
    }

    public void SetLevelStatus(GameObject _gameObject, LevelStatus levelStatus)
    {
        if (_gameObject.GetComponent<LevelHandler>() == null)
            return;
        string key = Constants.LevelStatusKey + currentLevel;
        PlayerPrefs.SetInt(key, (int)levelStatus);
    }

    public void SetLevelStatus(GameObject _gameObject, LevelStatus levelStatus, int level)
    {
        if (_gameObject.GetComponent<LevelHandler>() == null)
            return;

        string key = Constants.LevelStatusKey + level;
        PlayerPrefs.SetInt(key, (int)levelStatus);
    }

    public LevelStatus GetLevelStatus(int level)
    {
        string key = Constants.LevelStatusKey + level;
        if (level <= 1 && PlayerPrefs.GetInt(key, 0) == 0)
        {
            return LevelStatus.Unlocked;
        }
        return (LevelStatus)PlayerPrefs.GetInt(key, 0);
    }
}
