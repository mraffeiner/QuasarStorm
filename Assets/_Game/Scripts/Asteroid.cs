using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    public static event Action Destroyed;

    private Rigidbody2D Rigidbody { get; set; }

    private void Awake() => Rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        Rigidbody.velocity = Time.deltaTime * transform.up;
        transform.Rotate(0f, 0f, 10f);
    }

    private void OnDestroy() => Destroyed?.Invoke();
}
