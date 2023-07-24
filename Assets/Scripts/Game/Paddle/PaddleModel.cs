using UnityEngine;

public class PaddleModel : MonoBehaviour
{
    [SerializeField]
    GameObject BallPrefab;

    [SerializeField]
    int totalBalls = 100;

    [SerializeField]
    float speed = 10f;
    public float Speed { get { return speed; } }

    bool available;
    public bool Available { get { return available; } }

    BallController[] Balls;

    void Awake()
    {
        available = true;
        Balls = new BallController[0];
    }

    void Start()
    {
        createBallObjets();
    }

    void createBallObjets()
    {
        Balls = new BallController[totalBalls];

        PaddleView paddleView = gameObject.GetComponent<PaddleView>();
        if (paddleView == null)
            return;

        for (int i = 0; i < totalBalls; i++)
        {
            GameObject ball = Instantiate(BallPrefab, gameObject.transform.position, Quaternion.identity);
            Balls[i] = ball.GetComponent<BallController>();
            Balls[i].PaddleView = paddleView;
        }
    }

    public void Shoot()
    {
        // Don't Shoot if paddle isn't ready
        if (!available)
            return;

        for (int i = 0; i < Balls.Length; i++)
        {
            Balls[i].Shoot(Vector2.up);
        }
    }
}
