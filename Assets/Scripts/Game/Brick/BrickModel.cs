using UnityEngine;

public class BrickModel : MonoBehaviour
{
    [SerializeField]
    int hits = 1;
    public int Hits { get { return hits; } }

    [SerializeField]
    BrickTypes brickType = BrickTypes.Standard;

    void Awake()
    {
        switch (brickType)
        {
            case BrickTypes.Standard:
                hits *= 3;
                break;
            case BrickTypes.Bonus:
                hits *= 5;
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If BrickType is not unbreakable then only proceed
        if (brickType != BrickTypes.Unbreakable)
        {
            // Making sure the collion made by the ball
            if (collision.gameObject.GetComponent<BallModel>() != null)
            {
                // decreasing number of hits left on the bricks
                hits--;
            }
        }
    }
}
