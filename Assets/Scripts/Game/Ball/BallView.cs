using UnityEngine;

public class BallView : MonoBehaviour
{
    BallModel ballModel;
    BallController ballController;

    Vector2 screenBounds;
    float height, width;
    public float Height { get { return height; } }
    public float Width { get { return width; } }
    public Vector3 Position { get { return gameObject.transform.position; } }

    void Awake()
    {
        ballModel = gameObject.GetComponent<BallModel>();
        ballController = gameObject.GetComponent<BallController>();

        if (ballModel == null || ballController == null)
            throw new MissingComponentException();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        width = gameObject.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        height = gameObject.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void LateUpdate()
    {
        Vector3 position = gameObject.transform.position;

        // If Ball is in motion don't perform any position update
        if (ballModel.IsMoving)
        {
            // If ball goes out of the screen bounds then reset it back on paddle
            if (position.x != Mathf.Clamp(position.x, -screenBounds.x, screenBounds.x))
                ballModel.ResetMotion();
            else if (position.y != Mathf.Clamp(position.y, -screenBounds.y, screenBounds.y))
                ballModel.ResetMotion();
        }
        else
        {

            position = ballController.PaddleView.Position;
            position.y += ballController.PaddleView.Height + height;

            gameObject.transform.position = position;
        }
    }
}
