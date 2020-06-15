using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Projectile")]
public class ProjectileObject : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public AudioClip clip;
    public Material material;
    public float scale = 1f;
    public int damage = 1;
    public float speed = 5f;
    public float range = 10f;
    public float duration = 0f;
    public float cooldown = 0f;
    public bool hitsMultiple = false;
}
