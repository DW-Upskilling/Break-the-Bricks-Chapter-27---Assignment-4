using UnityEngine;

public class BrickModel : MonoBehaviour
{
    [SerializeField]
    int hits = 1;
    public int Hits { get { return hits; } }

    bool gotHit = false;
    public bool GotHit
    {
        get
        {
            bool _gotHit = gotHit;
            gotHit = false;
            return _gotHit;
        }
    }

    [SerializeField]
    BrickTypes brickType = BrickTypes.Standard;
    public BrickTypes BrickType { get { return brickType; } }

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
                // bool flag helpful to play audio in brickView
                gotHit = true;

                // decreasing number of hits left on the bricks
                hits--;
            }
        }
    }
}
