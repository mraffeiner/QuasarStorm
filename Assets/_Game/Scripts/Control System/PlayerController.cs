using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public event Action<ProjectileObject, Transform> ShootEvent;

    public Rigidbody2D Rigidbody { get; private set; }

    [SerializeField] private ShipDatabase unlockedShips = null;
    [SerializeField] private ProjectileDatabase unlockedProjectiles = null;
    [SerializeField] private Transform projectileSpawn = null;
    [SerializeField] private float deadzone = .1f;

    private ShipObject currentShip;
    private ProjectileObject selectedProjectile;

    private float horizontalInput;
    private float verticalInput;
    private bool shoot;
    private float shootCooldown;

    private void Awake() => Rigidbody = GetComponent<Rigidbody2D>();

    private void Start()
    {
        currentShip = unlockedShips.objects[0];
        selectedProjectile = unlockedProjectiles.objects[0];
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Previous Ship"))
            currentShip = PreviousInList(currentShip, unlockedShips.objects);
        if (Input.GetButtonDown("Next Ship"))
            currentShip = NextInList(currentShip, unlockedShips.objects);

        if (Input.GetButtonDown("Previous Weapon"))
            selectedProjectile = PreviousInList(selectedProjectile, unlockedProjectiles.objects);
        if (Input.GetButtonDown("Next Weapon"))
            selectedProjectile = NextInList(selectedProjectile, unlockedProjectiles.objects);

        if (!shoot && shootCooldown <= 0)
            shoot = Input.GetButton("Shoot");

        if (shootCooldown > 0)
            shootCooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(verticalInput) > deadzone)
            Move();

        if (Mathf.Abs(horizontalInput) > deadzone)
            Rotate();

        if (shoot && shootCooldown <= 0)
        {
            Shoot();
            shoot = false;
            shootCooldown = selectedProjectile.cooldown;
        }
    }

    private void Move() => Rigidbody.AddForce(transform.TransformDirection(Vector3.up * verticalInput) * currentShip.speed);

    private void Rotate() => Rigidbody.AddTorque(-horizontalInput * currentShip.handling);

    private void Shoot() => ShootEvent?.Invoke(selectedProjectile, projectileSpawn);

    private T NextInList<T>(T current, List<T> list)
    {
        var nextIndex = list.IndexOf(current) + 1;
        return nextIndex >= list.Count ? list[0] : list[nextIndex];
    }

    private T PreviousInList<T>(T current, List<T> list)
    {
        var previousIndex = list.IndexOf(current) - 1;
        return previousIndex < 0 ? list[list.Count - 1] : list[previousIndex];
    }
}
