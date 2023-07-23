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
    private int[] levelSceneIndices;

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
}
