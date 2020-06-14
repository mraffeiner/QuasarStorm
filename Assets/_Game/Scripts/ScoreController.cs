using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private IntVariable score = null;

    private void OnEnable() => Asteroid.Destroyed += OnAsteroidDestroyed;

    private void OnDisable() => Asteroid.Destroyed -= OnAsteroidDestroyed;

    private void OnAsteroidDestroyed() => score.value += 1;

    private void OnEnemyDied() => score.value += 2;

}
