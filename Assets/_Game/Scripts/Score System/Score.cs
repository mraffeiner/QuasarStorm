using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText = null;
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private IntVariable score = null;

    private int highScore;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = $"High Score: {highScore}";
    }

    private void Start() => score.value = 0;

    private void OnEnable() => HealthController.PlayerDied += OnPlayerDied;

    private void Update() => scoreText.text = $"Score: {score.value}";

    private void OnDisable() => HealthController.PlayerDied -= OnPlayerDied;

    private void OnPlayerDied()
    {
        if (score.value > highScore)
        {
            highScore = score.value;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }
}
