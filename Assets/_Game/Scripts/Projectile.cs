using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //TODO: Get layer mask that will be set from projectile spawner

    public ProjectileObject ProjectileStats { get; set; } = null;

    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;
    private bool _targetHit = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponentInChildren<CircleCollider2D>();
    }

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

        _rigidbody.velocity += (Vector2)transform.up * ProjectileStats.speed;

        while (!_targetHit && (elapsedTime < ProjectileStats.duration || Vector3.Distance(transform.position, startPosition) < ProjectileStats.range))
        {
            elapsedTime += Time.deltaTime;

            if (_rigidbody.velocity.magnitude > 0f && Vector3.Distance(transform.position, startPosition) >= ProjectileStats.range)
                _rigidbody.velocity = Vector2.zero;

            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}
