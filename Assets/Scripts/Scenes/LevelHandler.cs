using UnityEngine;
using TMPro;

public class LevelHandler : MonoBehaviour
{
    [SerializeField]
    int level = 0;
    [SerializeField]
    TextMeshProUGUI levelTextObject, scoreTextObject;

    void Awake()
    {
        if (levelTextObject != null)
        {
            levelTextObject.text = "Level: " + level;
        }

        if (scoreTextObject != null)
        {
            scoreTextObject.text = "Score: 0";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
