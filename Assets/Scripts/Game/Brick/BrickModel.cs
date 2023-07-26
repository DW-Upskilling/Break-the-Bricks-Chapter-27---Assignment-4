using UnityEngine;

public class BrickModel : MonoBehaviour
{
    [SerializeField]
    int hits = 1;
    public int Hits { get { return hits; } }

    [SerializeField]
    BrickTypes brickType = BrickTypes.Standard;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (brickType != BrickTypes.Unbreakable)
        {
            if (collision.gameObject.GetComponent<BallModel>() != null)
            {
                Debug.Log(hits);
                hits--;
            }
        }
    }
}
