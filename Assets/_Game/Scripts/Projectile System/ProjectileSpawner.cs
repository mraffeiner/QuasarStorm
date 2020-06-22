using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = null;

    private PlayerController playerController;
    private List<GameObject> projectilePool = new List<GameObject>();

    private void Awake() => playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

    private void OnEnable() => playerController.ShootEvent += OnShootEvent;

    private void OnDisable() => playerController.ShootEvent -= OnShootEvent;

    //TODO: Add target layermask to parameters
    private void OnShootEvent(ProjectileObject template, Transform spawnTransform)
    {
        // Instantiate / Redecorate with template values and spawn transform
        var projectileInstance = GetInactiveProjectile();
        projectileInstance.transform.position = spawnTransform.position;
        projectileInstance.transform.rotation = spawnTransform.rotation;
        projectileInstance.transform.localScale = Vector2.one * template.scale;

        var projectileComponent = projectileInstance.GetComponent<Projectile>();
        projectileComponent.ProjectileStats = template;

        // Set template sprite and add collider after to wrap around it automatically
        projectileComponent.SpriteRenderer.sprite = template.sprite;
        projectileComponent.SpriteRenderer.material = template.material;
        if (projectileInstance.GetComponent<CircleCollider2D>() == null)
        {
            var collider = projectileInstance.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
        }

        // Activate before setting velocity for physics to work properly
        projectileInstance.SetActive(true);
        projectileComponent.Rigidbody.velocity = playerController.Rigidbody.velocity;

        projectileComponent.StartProjectileAction();
    }

    private GameObject GetInactiveProjectile()
    {
        var inactiveProjectile = projectilePool.Find(x => !x.activeSelf);
        if (inactiveProjectile != null)
            return inactiveProjectile;

        var newProjectile = Instantiate(projectilePrefab, transform);
        newProjectile.SetActive(false);
        projectilePool.Add(newProjectile);

        return newProjectile;
    }
}
