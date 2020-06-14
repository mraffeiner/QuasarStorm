using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public event Action<ProjectileObject, Transform> ShootEvent;

    [SerializeField] private StarshipObject shipStats = null;
    [SerializeField] private ProjectileObject selectedProjectile = null;
    [SerializeField] private Transform projectileSpawn = null;
    [SerializeField] private float deadzone = .1f;

    new private Rigidbody2D rigidbody;
    private float horizontalInput;
    private float verticalInput;
    private bool shoot;

    private void Awake() => rigidbody = GetComponent<Rigidbody2D>();

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (!shoot)
            shoot = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0);

    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(verticalInput) > deadzone)
            Move();

        if (Mathf.Abs(horizontalInput) > deadzone)
            Rotate();

        if (shoot)
        {
            Shoot();
            shoot = false;
        }
    }

    private void Move() => rigidbody.AddForce(transform.TransformDirection(Vector3.up * verticalInput) * shipStats.speed);

    private void Rotate() => rigidbody.AddTorque(-horizontalInput * shipStats.handling);

    private void Shoot() => ShootEvent?.Invoke(selectedProjectile, projectileSpawn);
}
