using UnityEngine;
using TMPro;

public class BrickView : MonoBehaviour
{

    [SerializeField]
    Color color = Color.white;

    [SerializeField]
    TextMeshProUGUI hitsTextObject;

    BrickModel brickModel;

    void Awake()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        brickModel = gameObject.GetComponent<BrickModel>();
        if (brickModel == null || spriteRenderer == null)
            throw new MissingComponentException();

        // Setting color of the brick based on the type of the brick
        spriteRenderer.color = color;
    }

    void LateUpdate()
    {
        if (hitsTextObject != null)
            hitsTextObject.text = brickModel.Hits.ToString();

        // If number of hits left reaches 0 then break the brick(destory)
        if (brickModel.Hits <= 0)
        {
            Destroy(gameObject);
        }
    }
}
