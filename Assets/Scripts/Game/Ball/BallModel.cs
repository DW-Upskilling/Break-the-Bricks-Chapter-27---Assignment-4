using UnityEngine;

public class BallModel : MonoBehaviour
{
    [SerializeField]
    private float speed = 200, AxisXLeft = -9.5F, AxisXRight = 9.5F, AxisY = -4.5F;

    public float Speed { get { return speed; } }

    void Awake()
    {

    }
}
