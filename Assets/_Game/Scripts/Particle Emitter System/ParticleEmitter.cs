using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleEmitter : MonoBehaviour
{
    [SerializeField] private ParticleSystem burstParticles;

    private GameObject player;
    private MainModule burstMainModule;
    private ShapeModule burstShapeModule;

    private void Awake() => player = GameObject.FindWithTag("Player");

    private void Start()
    {
        burstMainModule = burstParticles.main;
        burstShapeModule = burstParticles.shape;
    }

    private void OnEnable()
    {
        HealthController.PlayerDied += OnPlayerDied;
        HealthController.DeathEvent += OnDeathEvent;
    }

    private void OnDisable()
    {

        HealthController.PlayerDied -= OnPlayerDied;
        HealthController.DeathEvent -= OnDeathEvent;
    }

    private void OnDeathEvent(GameObject deadObject) => DecorateBurstParticles(deadObject);

    private void OnPlayerDied() => DecorateBurstParticles(player);

    private void DecorateBurstParticles(GameObject deadObject)
    {
        var spriteRenderer = deadObject.GetComponent<SpriteRenderer>();

        transform.position = deadObject.transform.position;
        burstMainModule.startColor = new ParticleSystem.MinMaxGradient(spriteRenderer.color);
        burstShapeModule.spriteRenderer = spriteRenderer;

        burstParticles.Play();
    }
}
