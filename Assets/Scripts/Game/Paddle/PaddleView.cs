using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaddleView : MonoBehaviour
{

    [SerializeField]
    Color IdleColor = Color.yellow;
    [SerializeField]
    Color ActiveColor = Color.green;
    [SerializeField]
    Color BusyColor = Color.red;

    [SerializeField]
    TextMeshProUGUI speedTextObject, totalBallsTextObject;

    public Vector3 Position { get { return gameObject.transform.position; } }

    float width, height;
    public float Width { get { return width; } }
    public float Height { get { return height; } }

    PaddleModel paddleModel;
    PaddleController paddleController;

    Vector2 screenBounds;
    float AxisXLeft, AxisXRight, AxisY;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        paddleModel = gameObject.GetComponent<PaddleModel>();
        paddleController = gameObject.GetComponent<PaddleController>();

        if (paddleController == null || paddleModel == null || spriteRenderer == null)
            throw new MissingComponentException();

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
        GameObject mouseTracker = paddleModel.MouseTracker;
        if (mouseTracker == null)
            throw new MissingReferenceException();

        if (!paddleModel.AvailableToMove)
        {
            mouseTracker.SetActive(false);
            spriteRenderer.color = BusyColor;
            return;
        }
        else if (!paddleModel.AvailableToShoot)
        {
            spriteRenderer.color = IdleColor;
        }
        else
        {
            spriteRenderer.color = ActiveColor;
        }
        mouseTracker.SetActive(true);


        Vector2 position = gameObject.transform.position;
        position.x = Mathf.Clamp(position.x + paddleModel.PaddleSpeed * paddleController.Direction * Time.fixedDeltaTime, AxisXLeft, AxisXRight);
        position.y = AxisY;

        gameObject.transform.position = position;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(paddleController.MousePosition);
        mouseTracker.transform.position = mousePosition;

        if (speedTextObject != null)
            speedTextObject.text = "Speed: " + (Mathf.Clamp01(paddleModel.ShootForce / 3f) * 100f).ToString("F2") + "%";

        if (totalBallsTextObject)
            totalBallsTextObject.text = paddleModel.BallsOnPaddle.ToString();

    }
}
