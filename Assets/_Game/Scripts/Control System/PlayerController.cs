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

    [SerializeField] private RectTransform touchMoveRect = null;
    [SerializeField] private RectTransform touchShootRect = null;
    [SerializeField] private RectTransform touchSwapShipRect = null;
    [SerializeField] private RectTransform touchSwapWeaponRect = null;

    private ControlsBase controlModule;
    private ShipObject currentShip;
    private ProjectileObject currentWeapon;
    private float shootCooldown;

    private void Awake() => Rigidbody = GetComponent<Rigidbody2D>();

    private void Start()
    {
        currentShip = unlockedShips.objects[0];
        currentWeapon = unlockedProjectiles.objects[0];

        if (MobileDetection.isMobile())
            controlModule = new TouchControls(touchMoveRect, touchShootRect, touchSwapShipRect, touchSwapWeaponRect);
        else
            controlModule = new KeyboardControls();
    }

    private void Update()
    {
        controlModule.ReadInput();

        if (shootCooldown > 0)
            shootCooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(controlModule.verticalInput) > deadzone)
            Move();

        if (Mathf.Abs(controlModule.horizontalInput) > deadzone)
            Rotate();

        if (controlModule.shoot && shootCooldown <= 0)
        {
            Shoot();
            shootCooldown = currentWeapon.cooldown;
        }

        if (controlModule.cycleShip > 0)
            currentShip = NextInList(currentShip, unlockedShips.objects);
        if (controlModule.cycleShip < 0)
            currentShip = PreviousInList(currentShip, unlockedShips.objects);

        if (controlModule.cycleWeapon > 0)
            currentWeapon = NextInList(currentWeapon, unlockedProjectiles.objects);
        if (controlModule.cycleWeapon < 0)
            currentWeapon = PreviousInList(currentWeapon, unlockedProjectiles.objects);

        controlModule.ClearInput();
    }

    private void Move() => Rigidbody.AddForce(transform.TransformDirection(Vector3.up * controlModule.verticalInput) * currentShip.speed);

    private void Rotate() => Rigidbody.AddTorque(-controlModule.horizontalInput * currentShip.handling);

    private void Shoot() => ShootEvent?.Invoke(currentWeapon, projectileSpawn);

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
