using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private AsteroidSettings settings = null;
    [SerializeField] private GameObject asteroidPrefab = null;

    private TileManager tileSpawner;
    private List<GameObject> asteroidPool = new List<GameObject>();

    private void Awake() => tileSpawner = FindObjectOfType<TileManager>();

    private void OnEnable() => tileSpawner.TilePlaced += OnTilePlaced;

    private void OnDisble() => tileSpawner.TilePlaced -= OnTilePlaced;

    private void OnTilePlaced(GameObject tile)
    {
        var bounds = tile.GetComponent<SpriteRenderer>().sprite.bounds;

        // Instantiate random amount of asteroids with random positions, velocities, sprites and sizes
        for (int i = 0; i < Random.Range(settings.minSpawnsPerTile, settings.maxSpawnsPerTile + 1); i++)
        {
            // Set asteroid spawn to random position within the bounds of the moved tile
            var randomPointInBounds = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),       // x
                Random.Range(bounds.min.y, bounds.max.y));      // y
            var spawnPosition = (Vector2)tile.transform.position + randomPointInBounds;

            // Instantiate / Redecorate with random values depending on min / max settings
            var asteroidInstance = GetInactiveAsteroid();
            asteroidInstance.transform.position = spawnPosition;
            asteroidInstance.transform.localScale = Vector2.one * Random.Range(settings.minScale, settings.maxScale);

            // Set random sprite and add collider after to wrap around it automatically
            var asteroidComponent = asteroidInstance.GetComponent<Asteroid>();
            asteroidComponent.SpriteRenderer.sprite = settings.sprites[Random.Range(0, settings.sprites.Count)];
            // Destroy and readd collider to size it accurately
            if (asteroidInstance.GetComponent<PolygonCollider2D>() != null)
                Destroy(asteroidInstance.GetComponent<PolygonCollider2D>());
            asteroidInstance.AddComponent<PolygonCollider2D>();

            // Activate before setting velocity for physics to work properly
            asteroidInstance.SetActive(true);
            asteroidComponent.Rigidbody.velocity = new Vector2(
                Random.Range(settings.minMovementSpeed, settings.maxMovementSpeed),     // x
                Random.Range(settings.minMovementSpeed, settings.maxMovementSpeed));    // y
            asteroidComponent.Rigidbody.angularVelocity = Random.Range(settings.minRotationSpeed, settings.maxRotationSpeed);
        }
    }

    private GameObject GetInactiveAsteroid()
    {
        var inactiveAsteroid = asteroidPool.Find(x => !x.activeSelf);
        if (inactiveAsteroid != null)
            return inactiveAsteroid;

        var newAsteroid = Instantiate(asteroidPrefab);
        newAsteroid.SetActive(false);
        asteroidPool.Add(newAsteroid);

        return newAsteroid;
    }
}
