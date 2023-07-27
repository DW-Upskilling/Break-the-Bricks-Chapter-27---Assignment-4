using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;
    public static LevelManager Instance { get { return instance; } }

    // Maintains the index from the scenes in build
    [SerializeField]
    int[] levelSceneIndices;

    // Maintains the index of mainmenu in build
    [SerializeField]
    int mainMenuSceneIndex = 0;

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

    public int getLevelSceneId(int level)
    {
        if (level < 1 || level > Constants.TotalLevels)
            return -1;

        return levelSceneIndices[level - 1];
    }

    public void LoadScene(int level)
    {
        if (level < 1 || level > Constants.TotalLevels)
        {
            SceneManager.LoadScene(mainMenuSceneIndex);
        }
        else SceneManager.LoadScene(getLevelSceneId(level));
    }

}
