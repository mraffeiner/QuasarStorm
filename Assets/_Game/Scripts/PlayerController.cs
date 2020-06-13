using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public event Action<ProjectileObject, Transform> ShootEvent;

    [SerializeField] private StarshipObject _shipStats = null;
    [SerializeField] private ProjectileObject _selectedProjectile = null;
    [SerializeField] private Transform _projectileSpawn = null;
    [SerializeField] private float deadzone = .1f;

    private Rigidbody2D _rigidbody;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _shoot;

    private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (!_shoot)
            _shoot = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0);
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_verticalInput) > deadzone)
            Move();

        if (Mathf.Abs(_horizontalInput) > deadzone)
            Rotate();

        if (_shoot)
        {
            Shoot();
            _shoot = false;
        }

    }

    private void Move()
    {
        var movementVector = transform.TransformDirection(new Vector3(0f, _verticalInput, 0f));
        _rigidbody.AddForce(movementVector * _shipStats.speed);
    }

    private void Rotate()
    {
        _rigidbody.AddTorque(-_horizontalInput * _shipStats.handling);
    }

    private void Shoot() => ShootEvent?.Invoke(_selectedProjectile, _projectileSpawn);
}
