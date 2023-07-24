using UnityEngine;

public class BallModel : MonoBehaviour
{

    [SerializeField]
    PhysicsMaterial2D BallPhysicsMaterial2D;

    bool isMoving;
    public bool IsMoving { get { return isMoving; } }

    void Awake()
    {
        isMoving = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(Vector2.zero, ForceMode2D.Force);

        if (collision.gameObject.GetComponent<PaddleModel>() != null)
        {
            rigidbody2D.sharedMaterial = null;
            isMoving = false;
        }
    }

    public void Trigger(Vector2 direction)
    {
        if (isMoving == true)
            return;

        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null && BallPhysicsMaterial2D != null)
        {
            rigidbody2D.sharedMaterial = BallPhysicsMaterial2D;
            isMoving = true;

            rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
        }
    }

    public void ResetMotion()
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(Vector2.zero, ForceMode2D.Force);
        rigidbody2D.sharedMaterial = null;
        isMoving = false;
    }
}
