using UnityEngine;

public class BallView : MonoBehaviour
{
    BallModel ballModel;
    BallController ballController;

    Vector2 screenBounds;
    float height, width;
    public float Height { get { return height; } }
    public float Width { get { return width; } }

    void Awake()
    {
        ballModel = gameObject.GetComponent<BallModel>();
        ballController = gameObject.GetComponent<BallController>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        width = gameObject.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        height = gameObject.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void LateUpdate()
    {
        Vector3 position = gameObject.transform.position;

        if (position.x > screenBounds.x || position.x < -screenBounds.x || position.y > screenBounds.y || position.y < -screenBounds.y)
            ballModel.ResetMotion();

        // If Ball is in motion don't perform any position update
        if (ballModel.IsMoving)
            return;

        position = ballController.PaddleView.Position;
        position.y += ballController.PaddleView.Height + height;

        gameObject.transform.position = position;
    }
}
