using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private float direction = 0;
    public float Direction
    {
        get
        {
            float _direction = direction;
            direction = 0;
            return _direction;
        }
    }

    void Update()
    {
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
    }
}
