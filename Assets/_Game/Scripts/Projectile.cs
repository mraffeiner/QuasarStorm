using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO: Get layer mask that will be set from projectile spawner

    public ProjectileObject ProjectileStats { get; set; } = null;

    private CircleCollider2D _collider;
    private bool _targetHit = false;

    private void Awake() => _collider = GetComponentInChildren<CircleCollider2D>();

    private void OnTriggerEnter(Collider other)
    {
        //TODO: Handle collision distinction through layermasks
        if (other.transform.tag != "Player")
        {
            _targetHit = true;
            other.gameObject.GetComponent<HealthSystem>().TakeDamage(ProjectileStats.damage);
        }
    }

    public void StartProjectileAction() => StartCoroutine(ProjectileAction());

    //TODO: Make abstract for different flight paths
    private IEnumerator ProjectileAction()
    {
        var startPosition = transform.position;
        var elapsedTime = 0f;

        while (!_targetHit && Vector3.Distance(transform.position, startPosition) < ProjectileStats.range)
        {
            elapsedTime += Time.deltaTime;
            transform.position += Time.fixedDeltaTime * transform.up * ProjectileStats.speed;

            yield return null;
        }

        while (elapsedTime < ProjectileStats.duration)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Destroy(this.gameObject);
    }
}
