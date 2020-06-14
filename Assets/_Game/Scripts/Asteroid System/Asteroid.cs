using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    private Transform playerTransform;
    private WaitForSeconds checkInterval = new WaitForSeconds(5f);

    private Coroutine checkDistance;

    private void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        Rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() => checkDistance = StartCoroutine(CheckDistanceToPlayer());

    private void Start()
    {
        if (playerTransform == null)
            playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private IEnumerator CheckDistanceToPlayer()
    {
        while (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) < AsteroidSettings.simulationDistance)
            yield return checkInterval;

        gameObject.SetActive(false);
    }
}
