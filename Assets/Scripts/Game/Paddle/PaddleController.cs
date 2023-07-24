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

    void Awake()
    {
        direction = 0;
        paddleModel = gameObject.GetComponent<PaddleModel>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float jump = Input.GetAxisRaw("Jump");

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

        if (jump > 0)
        {
            paddleModel.Shoot();
        }
    }
}
