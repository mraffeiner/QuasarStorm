using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private IntVariable score = null;

    private void OnEnable() => HealthController.DeathEvent += OnDeathEvent;

    private void OnDisable() => HealthController.DeathEvent -= OnDeathEvent;

    private void OnDeathEvent() => score.value += 1;
}
