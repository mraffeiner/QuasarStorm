using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Asteroid Settings")]
public class AsteroidSettings : ScriptableObject
{
    public static float simulationDistance = 50f;

    public List<Sprite> sprites = new List<Sprite>();
    public int minSpawnsPerTile = 0;
    public int maxSpawnsPerTile = 5;
    public float minScale = 0f;
    public float maxScale = 5f;
    public float minMovementSpeed = 0f;
    public float maxMovementSpeed = 5f;
    public float minRotationSpeed = 0f;
    public float maxRotationSpeed = 5f;
}
