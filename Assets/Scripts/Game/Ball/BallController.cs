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

    void Awake()
    {
        ballModel = gameObject.GetComponent<BallModel>();
    }

    public void Shoot(Vector2 direction)
    {
        if (ballModel.IsMoving == false)
        {
            ballModel.Trigger(direction);
        }
    }
}
