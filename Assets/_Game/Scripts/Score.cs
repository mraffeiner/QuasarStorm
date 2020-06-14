using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{
    [SerializeField] private IntVariable score = null;

    private TextMeshProUGUI textMesh;

    private void Awake() => textMesh = GetComponent<TextMeshProUGUI>();

    private void Update() => textMesh.text = $"Score: {score.value}";
}
