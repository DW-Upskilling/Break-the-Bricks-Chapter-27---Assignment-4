using UnityEngine;

public class SessionManager : MonoBehaviour
{
    private static SessionManager instance = null;
    public static SessionManager Instance { get { return instance; } }

    private bool showMainMenu = false;
    public bool ShowMainMenu { get { return showMainMenu; } set { showMainMenu = value; } }

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

    public void SetLevelStatus(int level, LevelStatus levelStatus)
    {
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
