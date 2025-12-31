using UnityEngine;

public class EnemyHitboxController : MonoBehaviour
{
    [SerializeField] private Collider2D hitbox;

    void Awake()
    {
        if (hitbox != null) hitbox.enabled = false;
    }

    public void EnableHitbox() { if (hitbox != null) hitbox.enabled = true; }
    public void DisableHitbox() { if (hitbox != null) hitbox.enabled = false; }
}
