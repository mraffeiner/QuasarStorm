using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton = null;

    private void Start() => Cursor.visible = false;

    private void OnEnable() => HealthController.PlayerDied += OnPlayerDied;

    private void OnDisable() => HealthController.PlayerDied -= OnPlayerDied;

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    private void OnPlayerDied()
    {
        Cursor.visible = true;
        playButton.SetActive(true);
    }
}
