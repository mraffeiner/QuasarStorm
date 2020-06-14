using System.Collections;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private Sprite backgroundSprite = null;
    [SerializeField] private Color backgroundTint = new Color(255f, 255f, 255f, 255f);

    // Predefine boolean properties to make actual code more readable
    private bool TopPassed => mainCamera.transform.position.y > backgroundTopLeft.transform.position.y;
    private bool LeftPassed => mainCamera.transform.position.x < backgroundTopLeft.transform.position.x;
    private bool RightPassed => mainCamera.transform.position.x > backgroundBottomRight.transform.position.x;
    private bool BottomPassed => mainCamera.transform.position.y < backgroundBottomRight.transform.position.y;

    private Camera mainCamera;

    private GameObject backgroundTopLeft;
    private GameObject backgroundTopRight;
    private GameObject backgroundBottomLeft;
    private GameObject backgroundBottomRight;

    private float doubleSpriteWidth;
    private float doubleSpriteHeight;

    // Time between checking if the camera is leaving the background sprite bounds (cached to lower garbage buildup)
    private WaitForSeconds checkInterval = new WaitForSeconds(.1f);

    private void Start()
    {
        mainCamera = Camera.main;

        // Get screen bounds in world space to be able to compare them to sprite bounds based on position
        var screenBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        var screenHalfWidth = screenBounds.x / 2f;
        var screenHalfHeight = screenBounds.y / 2f;

        // Get half bounds for initial backgrounds spawn
        var spriteBounds = backgroundSprite.bounds;
        var spriteHalfWidth = backgroundSprite.bounds.extents.x;
        var spriteHalfHeight = backgroundSprite.bounds.extents.y;

        // Cache double bounds to prevent unnecessary calculations / allocations every loop step
        doubleSpriteWidth = spriteBounds.size.x * 2f;
        doubleSpriteHeight = spriteBounds.size.y * 2f;

        // Spawn 4 background instances centered around the player
        backgroundTopLeft = CreateBackground("BackgroundTopLeft", spriteHalfWidth, spriteHalfHeight);
        backgroundTopRight = CreateBackground("BackgroundTopRight", -spriteHalfWidth, spriteHalfHeight);
        backgroundBottomLeft = CreateBackground("BackgroundBottomLeft", spriteHalfWidth, -spriteHalfHeight);
        backgroundBottomRight = CreateBackground("BackgroundBottomRight", -spriteHalfWidth, -spriteHalfHeight);

        StartCoroutine(BoundaryCheckLoop());
    }

    private GameObject CreateBackground(string name, float xPosition, float yPosition)
    {
        // Create new game object with given parameters
        var background = new GameObject(name);
        background.transform.position = new Vector2(-xPosition, yPosition);

        // Add sprite renderer with defined settings
        var renderer = background.AddComponent<SpriteRenderer>();
        renderer.sortingLayerName = "Background";
        renderer.sprite = backgroundSprite;
        renderer.color = backgroundTint;

        return background;
    }

    // If the camera crosses any of the boundaries
    // then reposition the sprite that left the frame
    // in front of it's neighbor and swap their references
    private IEnumerator BoundaryCheckLoop()
    {
        while (true)
        {
            if (TopPassed)
            {
                ShiftAndSwap(ref backgroundBottomLeft, ref backgroundTopLeft, Vector3.up * doubleSpriteHeight);
                ShiftAndSwap(ref backgroundBottomRight, ref backgroundTopRight, Vector3.up * doubleSpriteHeight);
            }
            if (LeftPassed)
            {
                ShiftAndSwap(ref backgroundTopRight, ref backgroundTopLeft, Vector3.left * doubleSpriteWidth);
                ShiftAndSwap(ref backgroundBottomRight, ref backgroundBottomLeft, Vector3.left * doubleSpriteWidth);
            }
            if (RightPassed)
            {
                ShiftAndSwap(ref backgroundTopLeft, ref backgroundTopRight, Vector3.right * doubleSpriteWidth);
                ShiftAndSwap(ref backgroundBottomLeft, ref backgroundBottomRight, Vector3.right * doubleSpriteWidth);
            }
            if (BottomPassed)
            {
                ShiftAndSwap(ref backgroundTopLeft, ref backgroundBottomLeft, Vector3.down * doubleSpriteHeight);
                ShiftAndSwap(ref backgroundTopRight, ref backgroundBottomRight, Vector3.down * doubleSpriteHeight);
            }

            yield return checkInterval;
        }
    }

    private void ShiftAndSwap(ref GameObject shift, ref GameObject swapWith, Vector3 shiftVector)
    {
        shift.transform.position += shiftVector;

        var temp = shift;
        shift = swapWith;
        swapWith = temp;
    }
}
