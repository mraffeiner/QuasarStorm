using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioElement> audioElements = new List<AudioElement>();

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        foreach (var element in audioElements)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = element.clip;
            audioSource.volume = element.volume;
            audioSource.pitch = element.pitch;

            element.source = audioSource;
        }
    }

    private void Start()
    {
        var ambientRumble = audioElements.Find(x => x.clipName == "BackgroundMusic");
        ambientRumble.source.loop = true;
        ambientRumble.source.Play();
    }

    private void OnEnable()
    {
        HealthController.PlayerDied += OnPlayerDied;
        playerController.ShootEvent += OnShootEvent;
        HealthController.DeathEvent += OnDeathEvent;
    }

    private void OnDisable()
    {
        HealthController.PlayerDied -= OnPlayerDied;
        playerController.ShootEvent -= OnShootEvent;
        HealthController.DeathEvent -= OnDeathEvent;
    }

    private void OnPlayerDied() => audioElements.Find(x => x.clipName == "PlayerDeath")?.source.Play();

    private void OnShootEvent(ProjectileObject projectile, Transform spawn) => audioElements.Find(x => x.clip == projectile.clip)?.source.Play();

    //TODO: Make this genereric by adding a death clip to health controller or sth like that
    private void OnDeathEvent(GameObject deadObject) => audioElements.Find(x => x.clipName == "AsteroidExplosion")?.source.Play();
}
