using UnityEngine;

public class WallModel : MonoBehaviour
{
    [SerializeField]
    private GameObject BoundaryPrefab;

    void Awake()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Creating boundaries on all four sides of the screen based on the screen size
        GameObject left = Instantiate(BoundaryPrefab, gameObject.transform);
        GameObject right = Instantiate(BoundaryPrefab, gameObject.transform);
        GameObject top = Instantiate(BoundaryPrefab, gameObject.transform);
        GameObject bottom = Instantiate(BoundaryPrefab, gameObject.transform);

        // Getting size of the boundary object
        float width = top.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float height = top.transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        Vector3 position = top.transform.position;
        Vector3 localScale = top.transform.localScale;
        Vector3 eulerAngles = top.transform.eulerAngles;

        // top
        position.y = Mathf.Clamp(position.y, screenBounds.y + height, screenBounds.y * -1 - height);
        top.transform.position = position;

        // bottom
        position.y = -position.y;
        bottom.transform.position = position;

        position = left.transform.position;
        eulerAngles = left.transform.eulerAngles;
        eulerAngles.z = 90;

        // left
        position.x = Mathf.Clamp(position.y, screenBounds.x + width, screenBounds.x * -1 - width) / 2;
        left.transform.position = position;
        left.transform.eulerAngles = eulerAngles;

        // left
        position.x = -position.x;
        right.transform.position = position;
        right.transform.eulerAngles = eulerAngles;
    }
}
