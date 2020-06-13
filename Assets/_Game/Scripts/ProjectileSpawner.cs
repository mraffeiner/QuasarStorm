using UnityEngine;

//TODO: Implement object pooler for each unique projectile
public class ProjectileSpawner : MonoBehaviour
{
    private PlayerController _playerController;

    private void Awake() => _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

    private void OnEnable() => _playerController.ShootEvent += OnShootEvent;

    private void OnDisable() => _playerController.ShootEvent -= OnShootEvent;

    //TODO: Add target layermask to parameters
    private void OnShootEvent(ProjectileObject template, Transform spawnTransform)
    {
        var projectileInstance = Instantiate(template.prefab);
        projectileInstance.transform.position = spawnTransform.position;
        projectileInstance.transform.rotation = spawnTransform.rotation;
        projectileInstance.transform.localScale *= template.scale;

        var projectileComponent = projectileInstance.GetComponent<Projectile>();
        projectileComponent.ProjectileStats = template;

        projectileInstance.GetComponent<SpriteRenderer>().sprite = template.sprite;
        var collider = projectileInstance.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;


        projectileComponent.StartProjectileAction();
    }
}
