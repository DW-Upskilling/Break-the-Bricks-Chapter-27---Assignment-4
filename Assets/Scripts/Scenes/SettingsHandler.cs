using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsHandler : MonoBehaviour
{

    SessionManager sessionManager;
    LevelManager levelManager;

    void Start()
    {
        sessionManager = SessionManager.Instance;
        levelManager = LevelManager.Instance;
        if (sessionManager == null || levelManager == null)
            throw new MissingReferenceException("SessionManager, LevelManager");
    }
}
