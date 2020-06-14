using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton = null;

    private void OnEnable() => HealthController.PlayerDied += OnPlayerDied;

    private void OnDisable() => HealthController.PlayerDied -= OnPlayerDied;

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    private void OnPlayerDied() => playButton.SetActive(true);
}
