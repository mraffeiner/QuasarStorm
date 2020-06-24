using UnityEngine;

public class ProjectileSpawner : ObjectPoolBase
{
    private PlayerController playerController;

    private void Awake() => playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

    private void OnEnable() => playerController.ShootEvent += OnShootEvent;

    private void OnDisable() => playerController.ShootEvent -= OnShootEvent;

    //TODO: Add target layermask to parameters
    private void OnShootEvent(ProjectileObject template, Transform spawnTransform)
    {
        // Instantiate / Redecorate with template values and spawn transform
        var projectileInstance = GetInactiveFromPool();
        projectileInstance.transform.position = spawnTransform.position;
        projectileInstance.transform.rotation = spawnTransform.rotation;
        projectileInstance.transform.localScale = Vector2.one * template.scale;

        var projectileComponent = projectileInstance.GetComponent<Projectile>();
        projectileComponent.ProjectileStats = template;

        // Set template sprite and add collider after to wrap around it automatically
        projectileComponent.SpriteRenderer.sprite = template.sprite;
        projectileComponent.SpriteRenderer.material = template.material;

        // Destroy and readd collider to size it accurately
        if (projectileInstance.GetComponent<CircleCollider2D>() != null)
            Destroy(projectileInstance.GetComponent<CircleCollider2D>());
        projectileInstance.AddComponent<CircleCollider2D>().isTrigger = true;

        // Activate before setting velocity for physics to work properly
        projectileInstance.SetActive(true);
        projectileComponent.Rigidbody.velocity = playerController.Rigidbody.velocity;

        projectileComponent.StartProjectileAction();
    }
}
