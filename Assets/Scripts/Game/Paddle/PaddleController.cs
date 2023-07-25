using UnityEngine;

public class PaddleController : MonoBehaviour
{

    float direction;
    PaddleModel paddleModel;

    public float Direction
    {
        get
        {
            float _direction = direction;
            direction = 0;
            return _direction;
        }
    }

    Vector2 mousePosition;
    public Vector2 MousePosition { get { return mousePosition; } }

    void Awake()
    {
        direction = 0;
        paddleModel = gameObject.GetComponent<PaddleModel>();
        if (paddleModel == null)
            throw new MissingComponentException();
    }

    void Update()
    {
        mousePosition = Input.mousePosition;

        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0)
        {
            direction = -1;
        }
        else if (horizontal > 0)
        {
            direction = 1;
        }
        else
        {
            direction = 0;
        }

        float shoot = Input.GetAxisRaw("Fire1");
        if (shoot > 0 && paddleModel.AvailableToShoot && paddleModel.AvailableToMove)
        {
            paddleModel.Trigger();
        }
    }
}
