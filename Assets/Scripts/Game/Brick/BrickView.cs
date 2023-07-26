using UnityEngine;

public class BrickView : MonoBehaviour
{

    [SerializeField]
    Color color = Color.white;

    BrickModel brickModel;

    void Awake()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        brickModel = gameObject.GetComponent<BrickModel>();
        if (brickModel == null || spriteRenderer == null)
            throw new MissingComponentException();

        spriteRenderer.color = color;
    }

    void LateUpdate()
    {
        if (brickModel.Hits <= 0)
        {
            Destroy(gameObject);
        }
    }
}
