using UnityEngine;
using TMPro;

public class BrickView : MonoBehaviour
{

    [SerializeField]
    Color color = Color.white;

    [SerializeField]
    TextMeshProUGUI hitsTextObject;

    BrickModel brickModel;

    AudioManager audioManager;
    AudioSource BrickHitAudio, StandardBrickDoneAudio, BonusBrickDoneAudio;

    void Awake()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        brickModel = gameObject.GetComponent<BrickModel>();
        if (brickModel == null || spriteRenderer == null)
            throw new MissingComponentException("BrickModel, spriteRenderer");

        audioManager = AudioManager.Instance;
        if (audioManager == null)
            throw new MissingComponentException("AudioManager");

        // Setting color of the brick based on the type of the brick
        spriteRenderer.color = color;

        BrickHitAudio = audioManager.findAudio("BrickHit");
        StandardBrickDoneAudio = audioManager.findAudio("StandardBrickDone");
        BonusBrickDoneAudio = audioManager.findAudio("BonusBrickDone");
    }

    void LateUpdate()
    {
        if (brickModel.GotHit && BrickHitAudio != null)
            BrickHitAudio.Play();

        if (hitsTextObject != null)
            hitsTextObject.text = brickModel.Hits.ToString();

        // If number of hits left reaches 0 then break the brick(destory)
        if (brickModel.Hits <= 0)
        {
            switch (brickModel.BrickType)
            {
                case BrickTypes.Standard:
                    if (StandardBrickDoneAudio != null)
                        StandardBrickDoneAudio.Play();
                    break;
                case BrickTypes.Bonus:
                    if (BonusBrickDoneAudio != null)
                        BonusBrickDoneAudio.Play();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
