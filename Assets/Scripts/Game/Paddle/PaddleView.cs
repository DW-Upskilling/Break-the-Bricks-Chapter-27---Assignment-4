using UnityEngine;

public class PaddleView : MonoBehaviour
{
    public Vector3 Position { get { return gameObject.transform.position; } }

    float width, height;
    public float Width { get { return width; } }
    public float Height { get { return height; } }

    PaddleModel paddleModel;
    PaddleController paddleController;

    Vector2 screenBounds;
    float AxisXLeft, AxisXRight, AxisY;

    void Awake()
    {
        paddleModel = gameObject.GetComponent<PaddleModel>();
        paddleController = gameObject.GetComponent<PaddleController>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        width = gameObject.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        height = gameObject.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        AxisXLeft = -(screenBounds.x - width);
        AxisXRight = screenBounds.x - width;
        AxisY = -(screenBounds.y + height) * 0.75f;

        Vector3 position = transform.position;
        position.x = Random.Range(AxisXLeft, AxisXRight);
        position.y = AxisY;
    }

    void FixedUpdate()
    {
        Vector2 position = gameObject.transform.position;
        position.x = Mathf.Clamp(position.x + paddleModel.Speed * paddleController.Direction * Time.fixedDeltaTime, AxisXLeft, AxisXRight);
        position.y = AxisY;

        gameObject.transform.position = position;
    }
}
