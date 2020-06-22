using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuParent = null;

    private void OnEnable() => HealthController.PlayerDied += OnPlayerDied;

    private void Start() => SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);

    private void OnDisable() => HealthController.PlayerDied -= OnPlayerDied;

    private void OnPlayerDied()
    {
        menuParent.SetActive(true);
        Cursor.visible = true;
    }
}
