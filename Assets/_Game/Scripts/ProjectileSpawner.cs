using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = null;

    private PlayerController playerController;
    private Rigidbody2D playerRigidbody;
    private List<GameObject> projectilePool = new List<GameObject>();

    private void Awake()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerRigidbody = playerController.GetComponent<Rigidbody2D>();
    }

    private void OnEnable() => playerController.ShootEvent += OnShootEvent;

    private void OnDisable() => playerController.ShootEvent -= OnShootEvent;

    //TODO: Add target layermask to parameters
    private void OnShootEvent(ProjectileObject template, Transform spawnTransform)
    {
        var projectileInstance = GetInactiveProjectile();
        projectileInstance.transform.position = spawnTransform.position;
        projectileInstance.transform.rotation = spawnTransform.rotation;
        projectileInstance.transform.localScale *= template.scale;

        var projectileComponent = projectileInstance.GetComponent<Projectile>();
        projectileComponent.ProjectileStats = template;

        var projectileRigidbody = projectileInstance.GetComponent<Rigidbody2D>();

        projectileInstance.GetComponent<SpriteRenderer>().sprite = template.sprite;
        var collider = projectileInstance.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;

        projectileInstance.SetActive(true);
        projectileRigidbody.velocity = playerRigidbody.velocity;

        projectileComponent.StartProjectileAction();
    }

    private GameObject GetInactiveProjectile()
    {
        var inactiveProjectile = projectilePool.Find(x => !x.activeSelf);
        if (inactiveProjectile != null)
            return inactiveProjectile;

        var newProjectile = Instantiate(projectilePrefab);
        newProjectile.SetActive(false);

        return newProjectile;
    }
}
