using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private IntVariable score = null;

    private void OnEnable() => HealthController.DeathEvent += OnDeathEvent;

    private void OnDisable() => HealthController.DeathEvent -= OnDeathEvent;

    private void OnDeathEvent(GameObject deadObject) => score.value += 1 + (int)deadObject.transform.localScale.magnitude;
}
