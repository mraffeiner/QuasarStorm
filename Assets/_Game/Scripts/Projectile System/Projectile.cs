using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileObject ProjectileStats { get; set; } = null;
    public Rigidbody2D Rigidbody { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    private bool targetHit = false;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        targetHit = !ProjectileStats.hitsMultiple;
        other.gameObject.GetComponent<HealthController>().TakeDamage(ProjectileStats.damage);
    }

    public void StartProjectileAction() => StartCoroutine(ProjectileAction());

    //TODO: Make abstract for different flight paths
    private IEnumerator ProjectileAction()
    {
        var startPosition = transform.position;
        var elapsedTime = 0f;

        Rigidbody.velocity += (Vector2)transform.up * ProjectileStats.speed;

        while (!targetHit && (elapsedTime < ProjectileStats.duration || Vector3.Distance(transform.position, startPosition) < ProjectileStats.range))
        {
            elapsedTime += Time.deltaTime;

            if (Rigidbody.velocity.magnitude > 0f && Vector3.Distance(transform.position, startPosition) >= ProjectileStats.range)
                Rigidbody.velocity = Vector2.zero;

            yield return null;
        }

        targetHit = false;
        this.gameObject.SetActive(false);
    }
}
