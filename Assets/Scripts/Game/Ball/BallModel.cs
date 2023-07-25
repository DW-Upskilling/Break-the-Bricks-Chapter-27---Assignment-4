using UnityEngine;

public class BallModel : MonoBehaviour
{

    [SerializeField]
    PhysicsMaterial2D BallPhysicsMaterial2D;

    float shootForce = .2f;

    PaddleModel paddleModel;
    public PaddleModel PaddleModel { get { return paddleModel; } set { paddleModel = value; } }

    bool isMoving;
    public bool IsMoving { get { return isMoving; } }

    int id;
    public int Id { get { return id; } set { id = value; } }

    void Awake()
    {
        isMoving = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PaddleModel>() != null)
        {
            ResetMotion();
        }
    }

    public void Trigger(Vector2 direction)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null && BallPhysicsMaterial2D != null && !isMoving)
        {
            rigidbody2D.sharedMaterial = BallPhysicsMaterial2D;
            isMoving = true;

            rigidbody2D.AddForce(direction * shootForce, ForceMode2D.Impulse);
        }
    }

    public void ResetMotion()
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null && paddleModel != null)
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0f;
            rigidbody2D.sharedMaterial = null;
            isMoving = false;

            paddleModel.Rest(id);
        }
    }
}
