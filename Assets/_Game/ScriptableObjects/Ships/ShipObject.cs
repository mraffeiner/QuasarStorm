using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Ship")]
public class ShipObject : ScriptableObject
{
    public Color color = Color.white;
    public float speed = 10f;
    public float handling = .1f;
}
