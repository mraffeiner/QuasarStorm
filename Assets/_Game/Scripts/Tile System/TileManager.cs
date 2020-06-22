using System;
using System.Collections;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public event Action<GameObject> TilePlaced;

    [SerializeField] private Sprite sprite = null;
    [SerializeField] private Color color = new Color(255f, 255f, 255f, 50f);

    // Predefine boolean properties to make actual code more readable
    private bool TopPassed => mainCamera.transform.position.y > tileTopLeft.transform.position.y;
    private bool LeftPassed => mainCamera.transform.position.x < tileTopLeft.transform.position.x;
    private bool RightPassed => mainCamera.transform.position.x > tileBottomRight.transform.position.x;
    private bool BottomPassed => mainCamera.transform.position.y < tileBottomRight.transform.position.y;

    private Camera mainCamera;

    private GameObject tileTopLeft;
    private GameObject tileTopRight;
    private GameObject tileBottomLeft;
    private GameObject tileBottomRight;

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

        // Get half bounds for initial tile's spawn
        var spriteBounds = sprite.bounds;
        var spriteHalfWidth = sprite.bounds.extents.x;
        var spriteHalfHeight = sprite.bounds.extents.y;

        // Cache double bounds to prevent unnecessary calculations / allocations every loop step
        doubleSpriteWidth = spriteBounds.size.x * 2f;
        doubleSpriteHeight = spriteBounds.size.y * 2f;

        // Spawn 4 tile instances centered around the player
        tileTopLeft = CreateTile("TileTopLeft", spriteHalfWidth, spriteHalfHeight);
        tileTopRight = CreateTile("TileTopRight", -spriteHalfWidth, spriteHalfHeight);
        tileBottomLeft = CreateTile("TileBottomLeft", spriteHalfWidth, -spriteHalfHeight);
        tileBottomRight = CreateTile("TileBottomRight", -spriteHalfWidth, -spriteHalfHeight);

        StartCoroutine(BoundaryCheckLoop());
    }

    private GameObject CreateTile(string name, float xPosition, float yPosition)
    {
        // Create new game object with given parameters
        var newTile = new GameObject(name);
        newTile.transform.parent = transform;
        newTile.transform.position = new Vector2(-xPosition, yPosition);

        // Add sprite renderer with defined settings
        var renderer = newTile.AddComponent<SpriteRenderer>();
        renderer.sortingLayerName = "Background";
        renderer.sprite = sprite;
        renderer.color = color;

        TilePlaced?.Invoke(newTile);

        return newTile;
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
                ShiftAndSwapTile(ref tileBottomLeft, ref tileTopLeft, Vector3.up * doubleSpriteHeight);
                ShiftAndSwapTile(ref tileBottomRight, ref tileTopRight, Vector3.up * doubleSpriteHeight);
            }
            if (LeftPassed)
            {
                ShiftAndSwapTile(ref tileTopRight, ref tileTopLeft, Vector3.left * doubleSpriteWidth);
                ShiftAndSwapTile(ref tileBottomRight, ref tileBottomLeft, Vector3.left * doubleSpriteWidth);
            }
            if (RightPassed)
            {
                ShiftAndSwapTile(ref tileTopLeft, ref tileTopRight, Vector3.right * doubleSpriteWidth);
                ShiftAndSwapTile(ref tileBottomLeft, ref tileBottomRight, Vector3.right * doubleSpriteWidth);
            }
            if (BottomPassed)
            {
                ShiftAndSwapTile(ref tileTopLeft, ref tileBottomLeft, Vector3.down * doubleSpriteHeight);
                ShiftAndSwapTile(ref tileTopRight, ref tileBottomRight, Vector3.down * doubleSpriteHeight);
            }

            yield return checkInterval;
        }
    }

    private void ShiftAndSwapTile(ref GameObject tileToShift, ref GameObject otherTile, Vector3 shiftVector)
    {
        tileToShift.transform.position += shiftVector;

        TilePlaced?.Invoke(tileToShift);

        var temp = tileToShift;
        tileToShift = otherTile;
        otherTile = temp;

    }
}
