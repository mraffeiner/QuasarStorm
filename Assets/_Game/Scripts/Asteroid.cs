using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public static event Action Destroyed;

    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * transform.up;
        transform.Rotate(0f, 0f, 10f);
    }

    private void OnDestroy() => Destroyed?.Invoke();
}
