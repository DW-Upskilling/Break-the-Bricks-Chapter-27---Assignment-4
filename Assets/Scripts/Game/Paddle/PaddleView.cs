using UnityEngine;

public class PaddleView : MonoBehaviour
{
    PaddleModel paddleModel;
    PaddleController paddleController;

    void Awake()
    {
        paddleModel = gameObject.GetComponent<PaddleModel>();
        paddleController = gameObject.GetComponent<PaddleController>();
    }

    void LateUpdate()
    {
        Vector2 position = gameObject.transform.position;
        position.x = position.x + paddleModel.Speed * paddleController.Direction * Time.deltaTime;

        paddleModel.updatePosition(position);
    }
}
