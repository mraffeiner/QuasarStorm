using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private class TouchInfo
    {
        public Vector2 touchStartPosition;
        public Vector2 touchEndPosition;
        public Vector2 touchDragVector;
        public bool touchComplete;
    }

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

    private ShipObject currentShip;
    private ProjectileObject selectedProjectile;

    private float horizontalInput;
    private float verticalInput;
    private bool shoot;
    private float shootCooldown;

    private List<TouchInfo> touchInfos = new List<TouchInfo>();

    private void Awake() => Rigidbody = GetComponent<Rigidbody2D>();

    private void Start()
    {
        currentShip = unlockedShips.objects[0];
        selectedProjectile = unlockedProjectiles.objects[0];
    }

    private void Update()
    {
        if (MobileDetection.isMobile())
            ReadTouchInput();
        else
            ReadKeyboardInput();

        if (shootCooldown > 0)
            shootCooldown -= Time.deltaTime;
    }

    private void ReadTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touchInfos.Count <= i)
                touchInfos.Add(new TouchInfo());

            // Set touch information
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchInfos[i].touchComplete = false;
                    touchInfos[i].touchStartPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    touchInfos[i].touchDragVector = touch.position - touchInfos[i].touchStartPosition;
                    break;
                case TouchPhase.Ended:
                    touchInfos[i].touchEndPosition = touch.position;
                    touchInfos[i].touchDragVector = touchInfos[i].touchEndPosition - touchInfos[i].touchStartPosition;
                    touchInfos[i].touchComplete = true;
                    break;
            }
            // Check which part of the screen the touch input is in
            if (RectTransformUtility.RectangleContainsScreenPoint(touchMoveRect, touch.position, null))
            {
                horizontalInput = touchInfos[i].touchComplete ? 0 : touchInfos[i].touchDragVector.normalized.x;
                verticalInput = touchInfos[i].touchComplete ? 0 : touchInfos[i].touchDragVector.normalized.y;
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(touchShootRect, touch.position, null))
            {
                // Shoot
                if (!shoot && shootCooldown <= 0)
                    shoot = !touchInfos[i].touchComplete;
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(touchSwapShipRect, touch.position, null))
            {
                // Swap Ship
                if (touchInfos[i].touchComplete)
                    currentShip = NextInList(currentShip, unlockedShips.objects);
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(touchSwapWeaponRect, touch.position, null))
            {
                // Swap Weapon
                if (touchInfos[i].touchComplete)
                    selectedProjectile = NextInList(selectedProjectile, unlockedProjectiles.objects);
            }

            touchInfos[i].touchComplete = false;
        }
    }

    private void ReadKeyboardInput()
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
