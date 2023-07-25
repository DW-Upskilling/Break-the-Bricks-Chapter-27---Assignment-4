using System.Collections;
using UnityEngine;

public class PaddleModel : MonoBehaviour
{
    [SerializeField]
    GameObject BallPrefab, MouseTrackerPrefab;

    [SerializeField]
    int totalBalls = 100;

    [SerializeField]
    float speed = 10f;
    public float Speed { get { return speed; } }

    GameObject mouseTracker;
    public GameObject MouseTracker { get { return mouseTracker; } }

    bool availableToMove;
    public bool AvailableToMove { get { return availableToMove; } }
    public bool AvailableToShoot { get { return ballsOnPaddle == totalBalls; } }

    // List of BallController Objects
    BallController[] Balls;
    // Used for making sure balls beign used or not and keep track
    bool[] BallsTracker;
    int ballsOnPaddle;
    readonly object lockObject = new object();

    void Awake()
    {
        if (MouseTrackerPrefab == null || BallPrefab == null)
            throw new MissingReferenceException();

        mouseTracker = Instantiate(MouseTrackerPrefab);
        Balls = new BallController[0];
        BallsTracker = new bool[0];
        ballsOnPaddle = 0;
        availableToMove = true;
    }

    void Start()
    {
        Balls = new BallController[totalBalls];
        BallsTracker = new bool[totalBalls];

        PaddleView paddleView = gameObject.GetComponent<PaddleView>();
        if (paddleView == null)
            return;

        for (int i = 0; i < totalBalls; i++)
        {
            GameObject ball = Instantiate(BallPrefab, gameObject.transform.position, Quaternion.identity);
            Balls[i] = ball.GetComponent<BallController>();
            BallsTracker[i] = false;
            if (Balls[i] != null)
            {
                Balls[i].PaddleView = paddleView;
                Balls[i].PaddleModel = this;
                Balls[i].Id = i;
            }
        }

        ballsOnPaddle = totalBalls;
    }

    public void Trigger()
    {
        // Don't Shoot if paddle isn't ready
        if (!AvailableToShoot)
            return;

        StartCoroutine(Shoot());
    }

    public void Rest(int index)
    {
        if (index >= 0 && index < Balls.Length)
        {
            if (BallsTracker[index] != false)
            {
                ballsOnPaddle++;
                BallsTracker[index] = false;
            }
        }
    }

    IEnumerator Shoot()
    {
        Vector2 direction = mouseTracker.transform.position;
        availableToMove = false;
        for (int i = 0; i < Balls.Length; i++)
        {
            lock (lockObject)
            {
                BallsTracker[i] = true;
                ballsOnPaddle--;
                Balls[i].Shoot(direction);
                yield return new WaitForSeconds(0.01f);
            }
        }
        availableToMove = true;
    }

}
