using UnityEngine;

public class PaddleModel : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    Vector2 screenBounds;
    float width, height;
    float AxisXLeft, AxisXRight, AxisY;

    public float Speed { get { return speed; } }

    void Awake()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        width = gameObject.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        height = gameObject.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        AxisXLeft = -(screenBounds.x - width);
        AxisXRight = screenBounds.x - width;
        AxisY = -(screenBounds.y + height) * 0.75f;

        Vector3 position = transform.position;
        position.x = Random.Range(AxisXLeft, AxisXRight);

        updatePosition(position);
    }

    public void updatePosition(Vector3 newPosition)
    {
        // Paddle doesn't go out of screen in any case
        newPosition.x = Mathf.Clamp(newPosition.x, AxisXLeft, AxisXRight);
        newPosition.y = AxisY;

        gameObject.transform.position = newPosition;
    }
}
