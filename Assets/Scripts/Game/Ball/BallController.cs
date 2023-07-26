using UnityEngine;

public class BallController : MonoBehaviour
{
    BallModel ballModel;

    PaddleView paddleView;
    public PaddleView PaddleView
    {
        get { return paddleView; }
        set { paddleView = value; }
    }

    public PaddleModel PaddleModel
    {
        get { return ballModel.PaddleModel; }
        set { ballModel.PaddleModel = value; }
    }

    public int Id
    {
        get { return ballModel.Id; }
        set { ballModel.Id = value; }
    }

    Rigidbody2D _rigidbody2D;

    void Awake()
    {
        ballModel = gameObject.GetComponent<BallModel>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if (ballModel == null || _rigidbody2D == null)
            throw new MissingComponentException();
    }

    void Update()
    {
        if (_rigidbody2D.velocity.magnitude <= 0f)
        {
            ballModel.ResetMotion();
        }
    }

    public void Shoot(Vector2 direction)
    {
        if (ballModel.IsMoving == false)
        {
            ballModel.Trigger(direction);
        }
    }
}
